using System;
using System.Buffers;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;

namespace GuildWars2;

/// <summary>Represents a block of shared memory and provides an optimized way to copy that memory to a struct.</summary>
/// <remarks>Reference implementation:
/// <see
///     href="https://github.com/mumble-voip/mumble/blob/27e9552e896d8862bf75d96634f3295dfebb3196/plugins/link/LinkedMem.h" />
/// .</remarks>
internal sealed class MumbleLink : IDisposable
{
    /// <summary>The size of the memory-mapped file.</summary>
    /// <remarks>Obtained with Process Explorer.</remarks>
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

    [SupportedOSPlatform("windows")]
    public static MumbleLink CreateOrOpen(string name)
    {
        using var mutex = new Mutex(true, name + "_mutex", out var created);
        if (!created)
        {
            var acquired = mutex.WaitOne(TimeSpan.FromSeconds(5));
            if (!acquired)
            {
                throw new TimeoutException(
                    "Could not access the shared memory within the allotted time."
                );
            }
        }

        try
        {
            return new MumbleLink(
                MemoryMappedFile.CreateOrOpen(name, Length)
            );
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

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
