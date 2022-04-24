using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Mumble;

[PublicAPI]
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct Snapshot
{
    public readonly uint UiVersion;

    public readonly uint UiTick;

    /// <summary>Avatar position is the position of the player in the coordinate system of the map. While the game uses inches
    /// as unit, mumble uses meters.</summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public readonly float[] AvatarPosition;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public readonly float[] AvatarFront;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public readonly float[] AvatarTop;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public readonly string Name;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public readonly float[] CameraPosition;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public readonly float[] CameraFront;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public readonly float[] CameraTop;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public readonly string Identity;

    /// <summary>Despite the actual context containing more data, this value is always 48. This field tells Mumble to use the
    /// first 48 bytes to uniquely identify people on the same server shards. The remaining 208 bytes are purely informational.</summary>
    public readonly uint ContextLength;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    internal readonly byte[] Context;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
    public readonly string Description;

    public bool TryGetContext(out Context context)
    {
        context = default;
        if (Name != "Guild Wars 2")
        {
            return false;
        }

        var handle = GCHandle.Alloc(Context, GCHandleType.Pinned);
        try
        {
            var addr = handle.AddrOfPinnedObject();
            context = Marshal.PtrToStructure<Context>(addr);
        }
        finally
        {
            handle.Free();
        }

        return true;
    }

    public bool TryGetIdentity(
        [NotNullWhen(true)] out Identity? identity,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        identity = default;
        if (Name != "Guild Wars 2")
        {
            return false;
        }

        try
        {
            using var json = JsonDocument.Parse(Identity);
            identity = json.RootElement.GetIdentity(missingMemberBehavior);
            return true;
        }
        catch (JsonException)
        {
            // There is no synchronization between the game and Mumble, so we occassionally get a dirty read
            // The result of dirty reads is that we can get partially modified JSON that is potentially invalid
            //
            // Example: toggling between commander tag on/off might give a partial snapshot where 'true' and 'false' leak into each other
            //  "commander": frue,
            //
            //
            return false;
        }
    }
}
