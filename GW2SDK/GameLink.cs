using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;
using GuildWars2.Mumble;
using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
public sealed class GameLink : IDisposable, IObservable<GameTick>
{
    /// <summary>The smallest allowed polling interval.</summary>
    public readonly TimeSpan MinimumRefreshInterval = TimeSpan.FromMilliseconds(1);

    /// <summary>Represents the memory-mapped file used by the game client.</summary>
    private readonly MumbleLink mumbleLink;

    /// <summary>A list of observers who want to receive values.</summary>
    private readonly List<IObserver<GameTick>> subscribers = new();

    /// <summary>We don't get notified when the shared memory is updated, so we use a Timer to poll for changes.</summary>
    private readonly Timer timer;

    private bool busy;

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
        timer.Dispose();
        mumbleLink.Dispose();
    }

    public IDisposable Subscribe(IObserver<GameTick> observer)
    {
        // Ensure no duplicate subscriptions
        if (!subscribers.Contains(observer))
        {
            // Send an immediate snapshot to new subscribers
            var tick = GetSnapshot();
            if (tick.UiTick > 0)
            {
                observer.OnNext(tick);
            }

            subscribers.Add(observer);
        }

        return new Subscription(() => subscribers.Remove(observer));
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
            if (tick.UiTick != lastTick)
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

    public GameTick GetSnapshot() => mumbleLink.GetValue<GameTick>();
}
