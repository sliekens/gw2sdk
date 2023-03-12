using System;
using System.Buffers;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace GuildWars2;

/// <summary>Represents a block of shared memory and provides an optimized way to copy that memory to a struct.</summary>
internal sealed class MumbleLink : IDisposable
{
    /// <summary>The size of the memory-mapped file.</summary>
    private const int Length = 0x2000;

    private readonly byte[] buffer;

    private readonly GCHandle bufferHandle;

    private readonly MemoryMappedFile file;

    private readonly MemoryMappedViewAccessor view;

    private bool disposed;

    private MumbleLink(MemoryMappedFile file)
    {
        this.file = file;
        view = file.CreateViewAccessor(0, Length, MemoryMappedFileAccess.Read);
        buffer = ArrayPool<byte>.Shared.Rent(Length);
        bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
    }

    public void Dispose()
    {
        if (!disposed)
        {
            view.Dispose();
            file.Dispose();
            
            // ReSharper disable once PossiblyImpureMethodCallOnReadonlyVariable
            bufferHandle.Free();
            ArrayPool<byte>.Shared.Return(buffer);
        }

        disposed = true;
    }

#if NET
    [SupportedOSPlatform("windows")]
#endif
    public static MumbleLink CreateOrOpen(string name) =>
        new(MemoryMappedFile.CreateOrOpen(name, Length, MemoryMappedFileAccess.Read));

    public T GetValue<T>() where T : struct
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(MumbleLink));
        }

        var buffered = view.ReadArray(0, buffer, 0, Length);
        if (buffered != Length)
        {
            throw new InvalidOperationException(
                $"Expected {Length} bytes but received {buffered}. This usually indicates concurrent access to the current object, which is not supported."
            );
        }

        // ReSharper disable once PossiblyImpureMethodCallOnReadonlyVariable
        return Marshal.PtrToStructure<T>(bufferHandle.AddrOfPinnedObject());
    }
}
