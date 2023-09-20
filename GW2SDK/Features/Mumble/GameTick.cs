using System.Runtime.InteropServices;
using System.Text.Json;

namespace GuildWars2.Mumble;

[PublicAPI]
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
[NoReorder]
public readonly record struct GameTick
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

    public Identity? GetIdentity(
        MissingMemberBehavior missingMemberBehavior = MissingMemberBehavior.Error
    )
    {
        if (string.IsNullOrEmpty(Identity))
        {
            return null;
        }

        try
        {
            using var json = JsonDocument.Parse(Identity);
            return json.RootElement.GetIdentity(missingMemberBehavior);
        }
        catch (JsonException)
        {
            // There is a MINISCULE chance to receive mangled JSON while the
            //   shared memory is being updated by the game client
            return null;
        }
    }
}
