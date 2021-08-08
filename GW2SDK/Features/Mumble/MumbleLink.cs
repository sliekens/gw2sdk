using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using JetBrains.Annotations;
#if NET
using System.Runtime.Versioning;

#endif

namespace GW2SDK.Mumble
{
    [PublicAPI]
    public sealed class MumbleLink : IDisposable, IObservable<Snapshot>
    {
        public const int Length = 0x2000;

        private readonly byte[] _buffer;

        private readonly IntPtr _bufferAddress;

        private readonly MemoryMappedViewStream _content;

        private readonly MemoryMappedFile _file;

        private readonly List<IObserver<Snapshot>> _subscribers = new();

        private GCHandle _bufferHandle;

        private readonly Timer _timer;

        private long _lastIssued = -1;

        private MumbleLink(MemoryMappedFile file, TimeSpan refreshRate)
        {
            var safeInterval = TimeSpan.FromMilliseconds(20);
            var interval = refreshRate < safeInterval ? safeInterval : refreshRate;
            _timer = new Timer(interval.TotalMilliseconds)
            {
                AutoReset = false,
                Enabled = false
            };
            _timer.Elapsed += Publish;
            _timer.Disposed += RealDispose;
            _file = file;
            _content = file.CreateViewStream(0, Length, MemoryMappedFileAccess.Read);
            _buffer = ArrayPool<byte>.Shared.Rent(Length);
            _bufferHandle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);
            _bufferAddress = _bufferHandle.AddrOfPinnedObject();
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        public IDisposable Subscribe(IObserver<Snapshot> observer)
        {
            if (!_subscribers.Contains(observer))
            {
                _subscribers.Add(observer);
            }

            var subscription = new Subscription();
            subscription.Unsubscribed += (_, _) =>
            {
                _subscribers.Remove(observer);
            };

            _timer.Start();
            return subscription;
        }

        private void Publish(object sender, ElapsedEventArgs e)
        {
            var snapshot = GetSnapshot();
            if (snapshot.UiTick != _lastIssued)
            {
                _lastIssued = snapshot.UiTick;
                foreach (var subscriber in _subscribers.ToList())
                {
                    subscriber.OnNext(snapshot);
                }
            }

            if (_subscribers.Count != 0)
            {
                _timer.Start();
            }
        }

        private void RealDispose(object? sender, EventArgs e)
        {
            _content.Dispose();
            _file.Dispose();
            _bufferHandle.Free();
            ArrayPool<byte>.Shared.Return(_buffer);
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
        public static MumbleLink Open(TimeSpan refreshRate = default, string? name = "MumbleLink")
        {
            name ??= "MumbleLink";
            const long size = Length;
            var file = MemoryMappedFile.CreateOrOpen(name, size, MemoryMappedFileAccess.Read);
            return new MumbleLink(file, refreshRate);
        }

        public Snapshot GetSnapshot()
        {
#if NET
            var buffered = _content.Read(_buffer);
            if (buffered != Length)
            {
                throw new InvalidOperationException($"Expected {Length} bytes but received {buffered}.");
            }
#else
            var buffered = _content.Read(_buffer, 0, Length);
            if (buffered != Length)
            {
                throw new InvalidOperationException($"Expected {Length} bytes but received {buffered}.");
            }
#endif

            // Reset the view stream for the next call to GetSnapshot
            _content.Position = 0;

            return Marshal.PtrToStructure<Snapshot>(_bufferAddress);
        }

        private class Subscription : IDisposable
        {
            public void Dispose() => Unsubscribed?.Invoke(this, EventArgs.Empty);

            public event EventHandler? Unsubscribed;
        }
    }
}
