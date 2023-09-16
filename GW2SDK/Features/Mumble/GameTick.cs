using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text.Json;
using JetBrains.Annotations;

namespace GuildWars2.Mumble;

[PublicAPI]
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct GameTick
{
    public readonly uint UiVersion;

    public readonly uint UiTick;

    /// <summary>Avatar position is the position of the player in the coordinate system of the map. X is measured along the
    /// east-west axis, Y measures elevation, Z is measured along the north-south axis.</summary>
    /// <remarks>While the game uses inches as unit, mumble uses meters.</remarks>
    public readonly Vector3D AvatarPosition;

    public readonly Vector3D AvatarFront;

    public readonly Vector3D AvatarTop;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public readonly string Name;

    public readonly Vector3D CameraPosition;

    public readonly Vector3D CameraFront;

    public readonly Vector3D CameraTop;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public readonly string Identity;

    /// <summary>This field tells Mumble to use the first 48 bytes of the Context to uniquely identify people on the same
    /// server shards. It is internal because I don't expect anyone to find it useful.</summary>
    internal readonly uint ContextLength;

    public readonly Context Context;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
    public readonly string Description;

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
