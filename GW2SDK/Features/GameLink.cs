using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

using GuildWars2.Mumble;

namespace GuildWars2;

/// <summary>Represents a link to the game client that provides realtime game state updates.</summary>
[PublicAPI]
[CLSCompliant(false)]
public sealed class GameLink : IObservable<GameTick>, IDisposable, IAsyncDisposable
{
    /// <summary>The smallest allowed polling interval.</summary>
    public static readonly TimeSpan MinimumRefreshInterval = TimeSpan.FromMilliseconds(1);

    /// <summary>Represents the memory-mapped file used by the game client.</summary>
    private readonly MumbleLink mumbleLink;

    /// <summary>A list of observers who want to receive realtime game state.</summary>
    private readonly ConcurrentDictionary<IObserver<GameTick>, Subscription> subscribers = [];

#pragma warning disable CA2213 // Timer is properly disposed in Dispose() and DisposeAsync() methods
    /// <summary>A Timer is used to poll for changes to the shared memory as there is no push mechanism.</summary>
    private readonly Timer timer;
#pragma warning restore CA2213 // Timer is properly disposed in Dispose() and DisposeAsync() methods

    /// <summary>Flag indicating whether subscribers are being notified.</summary>
    private bool busy;

    private bool disposed;

    /// <summary>Used to keep track of the last UiTick that was published, to prevent repeating the same state.</summary>
    private uint lastTick;

    /// <summary>Initializes a new instance of the <see cref="GameLink" /> class.</summary>
    /// <param name="mumbleLink">The memory-mapped file used by the game client.</param>
    /// <param name="refreshInterval">The interval at which to poll for changes to the shared memory.</param>
    private GameLink(MumbleLink mumbleLink, in TimeSpan refreshInterval)
    {
        this.mumbleLink = mumbleLink;
        timer = new Timer(
            _ => Publish(),
            null,
            TimeSpan.Zero,
            TimeSpan.FromTicks(Math.Max(refreshInterval.Ticks, MinimumRefreshInterval.Ticks))
        );
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (disposed)
        {
            return;
        }

#if NET
        await timer.DisposeAsync().ConfigureAwait(false);
#else
        using ManualResetEventSlim callbacksFinished = new(false);
        if (timer.Dispose(callbacksFinished.WaitHandle))
        {
            TaskCompletionSource<bool> tcs = new();
#pragma warning disable CA1031 // Do not catch general exception types
            ThreadPool.QueueUserWorkItem(
                state =>
                {
                    (WaitHandle waitHandle, TaskCompletionSource<bool> taskCompletionSource) =
                        ((WaitHandle, TaskCompletionSource<bool>))state;
                    try
                    {
                        waitHandle.WaitOne();
                        taskCompletionSource.SetResult(true);
                    }
                    catch (Exception error)
                    {
                        taskCompletionSource.SetException(error);
                    }
                },
                (callbacksFinished.WaitHandle, tcs)
            );
#pragma warning restore CA1031 // Do not catch general exception types

            await tcs.Task.ConfigureAwait(false);
        }
#endif

        foreach (IObserver<GameTick>? subscriber in subscribers.Keys)
        {
            subscriber.OnCompleted();
        }

        mumbleLink.Dispose();
        disposed = true;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        // Disposing Timer is a bit tricky because a callback might be running at the moment
        // The solution is to pass a WaitHandle which the Timer will set when all callbacks have finished
        using ManualResetEventSlim callbacksFinished = new(false);

        // When Dispose returns true, wait for callbacks to finish and then do the cleanup
        // When Dispose returns false, it means Dispose was called twice, so do nothing
        if (timer.Dispose(callbacksFinished.WaitHandle))
        {
            callbacksFinished.WaitHandle.WaitOne();

            // Notify subscribers that there will be no more updates
            foreach (IObserver<GameTick>? subscriber in subscribers.Keys)
            {
                subscriber.OnCompleted();
            }

            // Close the shared memory AFTER OnCompleted
            // because observers might still attempt to use GetSnapshot() in OnCompleted
            mumbleLink.Dispose();

            // Set flag to prevent reusing a disposed instance
            disposed = true;
        }
    }

    /// <summary>Subscribes an observer to receive realtime game state updates.</summary>
    /// <param name="observer">The observer to subscribe.</param>
    /// <returns>An IDisposable object that can be used to unsubscribe the observer.</returns>
    public IDisposable Subscribe(IObserver<GameTick> observer)
    {
        if (disposed)
        {
            ThrowHelper.ThrowObjectDisposed(this);
        }

        // Ensure no duplicate subscriptions
        return subscribers.GetOrAdd(
            observer,
            sub =>
            {
#pragma warning disable CA1031 // Do not catch general exception types
                try
                {
                    GameTick tick = GetSnapshot();
                    if (tick.UiTick > 0)
                    {
                        sub.OnNext(tick);
                    }
                }
                catch (Exception oops)
                {
                    sub.OnError(oops);
                }
#pragma warning restore CA1031 // Do not catch general exception types

                return new Subscription(this, sub);
            }
        );
    }

    /// <summary>Gets a snapshot of the current game state.</summary>
    /// <returns>A <see cref="GameTick" /> object representing the current game state.</returns>
    public GameTick GetSnapshot()
    {
        if (disposed)
        {
            ThrowHelper.ThrowObjectDisposed(this);
        }

        return mumbleLink.GetValue<GameTick>();
    }

    /// <summary>Publishes the current game state to all subscribed observers.</summary>
    private void Publish()
    {
        if (busy || subscribers.IsEmpty)
        {
            return;
        }

        busy = true;
        try
        {
            GameTick tick;
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                tick = GetSnapshot();
            }
            catch (Exception reason)
            {
                // Notify every observer that there has been an internal error
                foreach (IObserver<GameTick>? subscriber in subscribers.Keys.ToList())
                {
                    try
                    {
                        subscriber.OnError(reason);
                    }
                    catch
                    {
                        // They did not respond well to the bad news
                    }
                }

                subscribers.Clear();
                return;
            }
#pragma warning restore CA1031 // Do not catch general exception types

            // The timer can be faster than the refresh rate of the shared memory
            // so ensure that the UiTick has changed, to avoid sending duplicates
            // This is especially important during loading screens or character selection
            if (tick.UiTick > 0 && tick.UiTick != lastTick)
            {
                lastTick = tick.UiTick;
                foreach (IObserver<GameTick>? subscriber in subscribers.Keys.ToList())
                {
#pragma warning disable CA1031 // Do not catch general exception types
                    try
                    {
                        subscriber.OnNext(tick);
                    }
                    catch (Exception reason)
                    {
                        // The observer threw an unhandled exception, which an observer should never do
                        try
                        {
                            // Notify them of their own error and then unsubscribe them
                            subscriber.OnError(reason);
                        }
                        catch
                        {
                            // At least we tried the diplomatic approach but they chose violence
                        }
                        finally
                        {
                            subscribers.TryRemove(subscriber, out _);
                        }
                    }
#pragma warning restore CA1031 // Do not catch general exception types
                }
            }
        }
        finally
        {
            busy = false;
        }
    }

    /// <summary>Checks if the GameLink is supported on the current platform.</summary>
    /// <returns><c>true</c> if the GameLink is supported on the current platform; otherwise, <c>false</c>.</returns>
    [SupportedOSPlatformGuard("windows")]
    public static bool IsSupported()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    /// <summary>Opens a GameLink instance with the specified refresh interval and name.</summary>
    /// <param name="refreshInterval">The interval at which to poll for changes to the shared memory.</param>
    /// <param name="name">The name of the memory-mapped file.</param>
    /// <returns>A new instance of the <see cref="GameLink" /> class.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown if the current platform is not Windows.</exception>
    [SupportedOSPlatform("windows")]
    public static GameLink Open(in TimeSpan refreshInterval = default, string name = "MumbleLink")
    {
        if (!IsSupported())
        {
            throw new PlatformNotSupportedException("Link is only supported on Windows.");
        }

        var link = MumbleLink.CreateOrOpen(name);
        return new GameLink(link, refreshInterval);
    }

    /// <summary>Represents a subscription to receive realtime game state updates.</summary>
    /// <param name="producer">The <see cref="GameLink"/> instance producing game state updates.</param>
    /// <param name="observer">The observer subscribing to game state updates.</param>
    private sealed class Subscription(GameLink producer, IObserver<GameTick> observer) : IDisposable
    {
        /// <summary>Disposes the subscription and removes the observer from the subscribers list.</summary>
        public void Dispose()
        {
            producer.subscribers.TryRemove(observer, out _);
        }
    }
}
