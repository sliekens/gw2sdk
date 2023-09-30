using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using GuildWars2.Mumble;

namespace GuildWars2;

[PublicAPI]
public sealed class GameLink : IObservable<GameTick>, IDisposable
{
    /// <summary>The smallest allowed polling interval.</summary>
    public static readonly TimeSpan MinimumRefreshInterval = TimeSpan.FromMilliseconds(1);

    /// <summary>Represents the memory-mapped file used by the game client.</summary>
    private readonly MumbleLink mumbleLink;

    /// <summary>A list of observers who want to receive realtime game state.</summary>
    private readonly List<IObserver<GameTick>> subscribers = new();

    /// <summary>A Timer is used to poll for changes to the shared memory as there is no push mechanism.</summary>
    private readonly Timer timer;

    /// <summary>Flag indicating whether subscribers are being notified.</summary>
    private bool busy;

    private bool disposed;

    /// <summary>Used to keep track of the last UiTick that was published, to prevent repeating the same state.</summary>
    private uint lastTick;

    private GameLink(MumbleLink mumbleLink, TimeSpan refreshInterval)
    {
        this.mumbleLink = mumbleLink;
        timer = new Timer(
            _ => Publish(),
            null,
            TimeSpan.Zero,
            TimeSpan.FromTicks(Math.Max(refreshInterval.Ticks, MinimumRefreshInterval.Ticks))
        );
    }

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
            foreach (var subscriber in subscribers)
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

    public IDisposable Subscribe(IObserver<GameTick> observer)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(GameLink));
        }

        // Ensure no duplicate subscriptions
        if (!subscribers.Contains(observer))
        {
            // Send an immediate snapshot to new subscribers
            try
            {
                var tick = GetSnapshot();
                if (tick.UiTick > 0)
                {
                    observer.OnNext(tick);
                }

                subscribers.Add(observer);
            }
            catch (Exception oops)
            {
                observer.OnError(oops);
            }
        }

        return new Subscription(this, observer);
    }

    public GameTick GetSnapshot()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(GameLink));
        }

        return mumbleLink.GetValue<GameTick>();
    }

    private void Publish()
    {
        if (busy || subscribers.Count == 0)
        {
            return;
        }

        busy = true;
        try
        {
            GameTick tick;
            try
            {
                tick = GetSnapshot();
            }
            catch (Exception reason)
            {
                // Notify every observer that there has been an internal error
                foreach (var subscriber in subscribers.ToList())
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

            // The timer can be faster than the refresh rate of the shared memory
            // so ensure that the UiTick has changed, to avoid sending duplicates
            // This is especially important during loading screens or character selection
            if (tick.UiTick > 0 && tick.UiTick != lastTick)
            {
                lastTick = tick.UiTick;
                foreach (var subscriber in subscribers.ToList())
                {
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
                            subscribers.Remove(subscriber);
                        }
                    }
                }
            }
        }
        finally
        {
            busy = false;
        }
    }

    [SupportedOSPlatformGuard("windows")]
    public static bool IsSupported() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    [SupportedOSPlatform("windows")]
    public static GameLink Open(TimeSpan refreshInterval = default, string name = "MumbleLink")
    {
        if (!IsSupported())
        {
            throw new PlatformNotSupportedException("Link is only supported on Windows.");
        }

        var link = MumbleLink.CreateOrOpen(name);
        return new GameLink(link, refreshInterval);
    }

    private class Subscription : IDisposable
    {
        private readonly GameLink producer;

        private readonly IObserver<GameTick> observer;

        public Subscription(GameLink producer, IObserver<GameTick> observer)
        {
            this.producer = producer;
            this.observer = observer;
        }

        public void Dispose() => producer.subscribers.Remove(observer);
    }
}
