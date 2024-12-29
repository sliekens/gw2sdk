using System.Numerics;
using System.Runtime.InteropServices;
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Mumble;

/// <summary>Represents the game state at a specific point in time.</summary>
[PublicAPI]
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
[NoReorder]
public readonly record struct GameTick
{
    /// <summary>The version of the MumbleLink plugin. This is used to determine the layout of the data structure. You may also
    /// check that it is not 0 to verify whether the Link is initialized.</summary>
    public readonly uint UiVersion;

    /// <summary>The current tick number. This is incremented every frame, and is used internally to detect if the data has
    /// changed since the last tick.</summary>
    /// <remarks>This number is sometimes reset to 0, like after loading a different map. Do not rely on it to be unique or
    /// always incrementing.</remarks>
    public readonly uint UiTick;

    /// <summary>Avatar position is the position of the player in the map coordinate system. X is measured along the
    /// east-west axis, Y measures elevation, Z is measured along the north-south axis.</summary>
    /// <remarks>The values are in meters and must be converted to inches to use them like other coordinates in the game.</remarks>
    public readonly Vector3 AvatarPosition;

    /// <summary>Unit vector pointing out of the avatar's eyes aka "At"-vector.</summary>
    /// <remarks>The values are in meters and must be converted to inches to use them like other coordinates in the game.</remarks>
    public readonly Vector3 AvatarFront;

    /// <summary>Unit vector pointing out of the top of the avatar's head aka "Up"-vector (here Top points straight up</summary>
    /// <remarks>The values are in meters and must be converted to inches to use them like other coordinates in the game.</remarks>
    public readonly Vector3 AvatarTop;

    /// <summary>The name of the game.</summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public readonly string Name;

    /// <summary>Same as <see cref="AvatarPosition" /> but for the camera.</summary>
    /// <remarks>The values are in meters and must be converted to inches to use them like other coordinates in the game.</remarks>
    public readonly Vector3 CameraPosition;

    /// <summary>Same as <see cref="AvatarFront" /> but for the camera.</summary>
    /// <remarks>The values are in meters and must be converted to inches to use them like other coordinates in the game.</remarks>
    public readonly Vector3 CameraFront;

    /// <summary>Same as <see cref="AvatarTop" /> but for the camera.</summary>
    /// <remarks>The values are in meters and must be converted to inches to use them like other coordinates in the game.</remarks>
    public readonly Vector3 CameraTop;

    /// <summary>A free-text field which uniquely identifies the player. The Guild Wars 2 client stores JSON in this field. Use
    /// <see cref="GetIdentity" /> to get the object representation.</summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public readonly string Identity;

    /// <summary>This field tells Mumble to use the first 48 bytes of the Context to uniquely identify people on the same
    /// server shards. It is internal because I don't expect anyone to find it useful.</summary>
    internal readonly uint ContextLength;

    /// <summary>Context which is used to uniquely identify people on the same server shards. It also contains miscellaneous UI
    /// states.</summary>
    public readonly Context Context;

    /// <summary>The description of the game.</summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
    public readonly string Description;

    /// <summary>Gets the identity object from the JSON string.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <returns>The player's identity.</returns>
    public Identity? GetIdentity(MissingMemberBehavior missingMemberBehavior = default)
    {
        if (string.IsNullOrEmpty(Identity))
        {
            return null;
        }

        try
        {
            using var json = JsonDocument.Parse(Identity);
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            return json.RootElement.GetIdentity();
        }
        catch (JsonException)
        {
            // There is a MINISCULE chance to receive mangled JSON while the
            //   shared memory is being updated by the game client
            return null;
        }
    }
}
