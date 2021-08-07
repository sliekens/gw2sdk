using System;
using System.Buffers;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
#if NET
using System.Runtime.Versioning;

#endif

namespace GW2SDK.Mumble
{
    public sealed class MumbleLink : IDisposable
    {
        public const int Length = 0x2000;

        private readonly byte[] _buffer;

        private readonly IntPtr _bufferAddress;

        private readonly MemoryMappedViewStream _content;

        private readonly MemoryMappedFile _file;

        private GCHandle _bufferHandle;

        private MumbleLink(MemoryMappedFile file)
        {
            _file = file;
            _content = file.CreateViewStream(0, Length, MemoryMappedFileAccess.Read);
            _buffer = ArrayPool<byte>.Shared.Rent(Length);
            _bufferHandle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);
            _bufferAddress = _bufferHandle.AddrOfPinnedObject();
        }

        public void Dispose()
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
        public static MumbleLink Open(string? name = "MumbleLink")
        {
            name ??= "MumbleLink";
            const long size = Length;
            var file = MemoryMappedFile.CreateOrOpen(name, size, MemoryMappedFileAccess.Read);
            return new MumbleLink(file);
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
    }
}
