using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using GuildWars2.Mumble;
using JetBrains.Annotations;
#if NET
using System.Runtime.Versioning;

#elif NETSTANDARD
using System.Runtime.InteropServices;
#endif

namespace GuildWars2;

[PublicAPI]
public sealed class GameLink : IDisposable, IObservable<GameTick>
{
    /// <summary>The smallest allowed polling interval.</summary>
    public readonly TimeSpan MinimumRefreshInterval = TimeSpan.FromMilliseconds(1d);

    /// <summary>Represents the memory-mapped file used by the game client.</summary>
    private readonly MumbleLink mumbleLink;

    /// <summary>A list of observers who want to receive values.</summary>
    private readonly List<IObserver<GameTick>> subscribers = new();

    /// <summary>We don't get notified when the shared memory is updated, so we use a Timer to poll for changes.</summary>
    private readonly Timer timer;

    /// <summary>The tick of the last snapshot that was published to observers.</summary>
    private long lastTick = -1;

    private GameLink(MumbleLink mumbleLink, TimeSpan refreshInterval)
    {
        this.mumbleLink = mumbleLink;
        timer = new Timer(
            Math.Max(refreshInterval.TotalMilliseconds, MinimumRefreshInterval.TotalMilliseconds)
        )
        {
            AutoReset = false,
            Enabled = false
        };
        timer.Elapsed += (_, _) => Publish();
        timer.Disposed += (_, _) => mumbleLink.Dispose();
    }

    public void Dispose()
    {
        subscribers.Clear();
        timer.Stop();
        timer.Dispose();
    }

    public IDisposable Subscribe(IObserver<GameTick> observer)
    {
        if (!subscribers.Contains(observer))
        {
            subscribers.Add(observer);
        }

        if (!timer.Enabled)
        {
            timer.Start();
        }

        return new Subscription(() => subscribers.Remove(observer));
    }

    private void Publish()
    {
        if (subscribers.Count == 0)
        {
            // Not sure if this is possible, the timer only starts after someone subscribes
            return;
        }

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

        timer.Start();
    }

#if NET
    [SupportedOSPlatformGuard("windows")]
#endif
    public static bool IsSupported()
    {
#if NET
        return OperatingSystem.IsWindows();
#elif NETFRAMEWORK
        return Environment.OSVersion.Platform == PlatformID.Win32NT;
#elif NETSTANDARD
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#endif
    }

#if NET
    [SupportedOSPlatform("windows")]
#endif
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
