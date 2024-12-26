﻿using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using GuildWars2.Mumble;

namespace GuildWars2;

/// <summary>Represents a link to the game client that provides realtime game state updates.</summary>
[PublicAPI]
public sealed class GameLink : IObservable<GameTick>, IDisposable
{
    /// <summary>The smallest allowed polling interval.</summary>
    public static readonly TimeSpan MinimumRefreshInterval = TimeSpan.FromMilliseconds(1);

    /// <summary>Represents the memory-mapped file used by the game client.</summary>
    private readonly MumbleLink mumbleLink;

    /// <summary>A list of observers who want to receive realtime game state.</summary>
    private readonly List<IObserver<GameTick>> subscribers = [];

    /// <summary>A Timer is used to poll for changes to the shared memory as there is no push mechanism.</summary>
    private readonly Timer timer;

    /// <summary>Flag indicating whether subscribers are being notified.</summary>
    private bool busy;

    private bool disposed;

    /// <summary>Used to keep track of the last UiTick that was published, to prevent repeating the same state.</summary>
    private uint lastTick;

    /// <summary>Initializes a new instance of the <see cref="GameLink" /> class.</summary>
    /// <param name="mumbleLink">The memory-mapped file used by the game client.</param>
    /// <param name="refreshInterval">The interval at which to poll for changes to the shared memory.</param>
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

    /// <summary>Disposes the <see cref="GameLink" /> instance and releases any resources used.</summary>
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
            // Give the timer 10s to finish all callbacks
            // This is a very generous timeout, it should never take this long
            // If it does, it's likely due to a deadlock in the callbacks
            if (!callbacksFinished.WaitHandle.WaitOne(10_000))
            {
                Debug.WriteLine("Timed out waiting for timer callbacks to finish, possibly due to a deathlock.");
            }

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

    /// <summary>Checks if the GameLink is supported on the current platform.</summary>
    /// <returns><c>true</c> if the GameLink is supported on the current platform; otherwise, <c>false</c>.</returns>
    [SupportedOSPlatformGuard("windows")]
    public static bool IsSupported() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    /// <summary>Opens a GameLink instance with the specified refresh interval and name.</summary>
    /// <param name="refreshInterval">The interval at which to poll for changes to the shared memory.</param>
    /// <param name="name">The name of the memory-mapped file.</param>
    /// <returns>A new instance of the <see cref="GameLink" /> class.</returns>
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

    /// <summary>Represents a subscription to receive realtime game state updates.</summary>
    private class Subscription(GameLink producer, IObserver<GameTick> observer) : IDisposable
    {
        /// <summary>Disposes the subscription and removes the observer from the subscribers list.</summary>
        public void Dispose() => producer.subscribers.Remove(observer);
    }
}
