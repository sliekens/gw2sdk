using System.Diagnostics.CodeAnalysis;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes;

namespace GuildWars2;

/// <summary>Represents a block of shared memory and provides an optimized way to copy that memory to a struct.</summary>
/// <remarks>Reference implementation:
/// <see
///     href="https://github.com/mumble-voip/mumble/blob/27e9552e896d8862bf75d96634f3295dfebb3196/plugins/link/LinkedMem.h" />
/// .</remarks>
internal sealed class MumbleLink : IDisposable
{
    /// <summary>The size of the shared memory struct.</summary>
    private const int Length = 5460;

    private readonly MemoryMappedFile file;

    private readonly MemoryMappedViewAccessor view;

    private bool disposed;

    private MumbleLink(MemoryMappedFile file)
    {
        this.file = file;
        view = file.CreateViewAccessor(0, Length, MemoryMappedFileAccess.Read);
    }

    public void Dispose()
    {
        if (!disposed)
        {
            view.Dispose();
            file.Dispose();
        }

        disposed = true;
    }

    [SupportedOSPlatform("windows")]
    public static MumbleLink CreateOrOpen(string name)
    {
        using Mutex mutex = new(true, name + "_mutex", out var created);
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
            return new MumbleLink(MemoryMappedFile.CreateOrOpen(name, Length));
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    public T GetValue<[DynamicallyAccessedMembers(PublicConstructors | NonPublicConstructors)] T>()
        where T : struct
    {
        if (disposed)
        {
            ThrowHelper.ThrowObjectDisposed(this);
        }

        var success = false;
        try
        {
            view.SafeMemoryMappedViewHandle.DangerousAddRef(ref success);
            var location = view.SafeMemoryMappedViewHandle.DangerousGetHandle();
            return Marshal.PtrToStructure<T>(location);
        }
        finally
        {
            if (!success)
            {
                view.SafeMemoryMappedViewHandle.DangerousRelease();
            }
        }
    }
}
