using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using GW2SDK.Mumble;
using JetBrains.Annotations;
#if NET
using System.Runtime.Versioning;

#endif

namespace GW2SDK
{
    [PublicAPI]
    public sealed class GameLink : IDisposable, IObservable<Snapshot>
    {
        public const int Length = 0x2000;

        private readonly byte[] buffer;

        private readonly IntPtr bufferAddress;

        private readonly MemoryMappedViewStream content;

        private readonly MemoryMappedFile file;

        private readonly byte[] integrityBuffer;

        private readonly List<IObserver<Snapshot>> subscribers = new();

        private readonly Timer timer;

        private GCHandle bufferHandle;

        private long lastIssued = -1;

        private GameLink(MemoryMappedFile file, TimeSpan refreshRate)
        {
            this.file = file;
            var safeInterval = TimeSpan.FromMilliseconds(1);
            var interval = refreshRate < safeInterval ? safeInterval : refreshRate;
            timer = new Timer(interval.TotalMilliseconds)
            {
                AutoReset = false,
                Enabled = false
            };
            timer.Elapsed += Publish;
            timer.Disposed += RealDispose;
            content = file.CreateViewStream(0, Length, MemoryMappedFileAccess.Read);
            buffer = ArrayPool<byte>.Shared.Rent(Length);
            integrityBuffer = ArrayPool<byte>.Shared.Rent(Length);
            bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            bufferAddress = bufferHandle.AddrOfPinnedObject();
        }

        public void Dispose()
        {
            timer.Stop();
            timer.Dispose();
        }

        public IDisposable Subscribe(IObserver<Snapshot> observer)
        {
            if (!subscribers.Contains(observer))
            {
                subscribers.Add(observer);
            }

            Subscription subscription = new();
            subscription.Unsubscribed += (_, _) =>
            {
                subscribers.Remove(observer);
            };

            timer.Start();
            return subscription;
        }

        private void Publish(object? sender, ElapsedEventArgs e)
        {
            try
            {
                var snapshot = GetSnapshot();
                if (snapshot.UiTick != lastIssued)
                {
                    lastIssued = snapshot.UiTick;
                    foreach (var subscriber in subscribers.ToList())
                    {
                        subscriber.OnNext(snapshot);
                    }
                }

                if (subscribers.Count != 0)
                {
                    timer.Start();
                }
            }
            catch (Exception reason)
            {
                foreach (var subscriber in subscribers.ToList())
                {
                    subscriber.OnError(reason);
                }

                subscribers.Clear();
            }
        }

        private void RealDispose(object? sender, EventArgs e)
        {
            content.Dispose();
            file.Dispose();
            bufferHandle.Free();
            ArrayPool<byte>.Shared.Return(integrityBuffer);
            ArrayPool<byte>.Shared.Return(buffer);
        }

        public static bool IsSupported()
        {
#if NET
            return OperatingSystem.IsWindows();
#elif NETSTANDARD
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#elif NET461
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
#endif
        }

#if NET
        [SupportedOSPlatform("windows")]
#endif
        public static GameLink Open(TimeSpan refreshRate = default, string? name = "MumbleLink")
        {
            name ??= "MumbleLink";
            const long size = Length;
            var file = MemoryMappedFile.CreateOrOpen(name, size, MemoryMappedFileAccess.ReadWrite);
            return new GameLink(file, refreshRate);
        }

        public Snapshot GetSnapshot()
        {
            // First buffer the entire content
            // Then buffer the entire content again to check for integrity errors
            // Note the need to specify the length because we use pooled arrays
            var next = buffer.AsSpan(0, Length);
            var control = integrityBuffer.AsSpan(0, Length);

            BufferContent(buffer);

            // This check is designed to detect dirty reads
            // Read the memory mapped file again and again, until we get the same result 5 times in a row
            // The number 5 seems magic but that's the minimum number of checks before I stopped getting invalid results
            for (var samenessCount = 0; samenessCount < 4; samenessCount++)
            {
                BufferContent(integrityBuffer);
                if (!next.SequenceEqual(control))
                {
                    // Change detected so replace the buffer with the latest contents of the memory mapped file, then reset the sameness counter
                    control.CopyTo(next);
                    samenessCount = 0;
                }
            }

            return Marshal.PtrToStructure<Snapshot>(bufferAddress);
        }

        private void BufferContent(byte[] destination)
        {
            try
            {
                // Note the need to specify the length because we use pooled arrays
#if NET
                var buffered = content.Read(destination.AsSpan(0, Length));

#else
                var buffered = content.Read(destination, 0, Length);
#endif
                if (buffered != Length)
                {
                    throw new InvalidOperationException(
                        $"Expected {Length} bytes but received {buffered}. This can happen when GetSnapshot is called concurrently because this class is not thread-safe. Synchronize access to GetSnapshot, or use multiple instances of GameLink to avoid this error."
                    );
                }
            }
            finally
            {
                // Reset the view stream for the next usage
                content.Position = 0;
            }
        }

        private class Subscription : IDisposable
        {
            public void Dispose() => Unsubscribed?.Invoke(this, EventArgs.Empty);

            public event EventHandler? Unsubscribed;
        }
    }
}
