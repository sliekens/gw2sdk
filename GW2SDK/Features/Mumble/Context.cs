using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.InteropServices;
using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Win32;

namespace GuildWars2.Mumble;

/// <summary>Context information provided by the MumbleLink API.</summary>
[PublicAPI]
[StructLayout(LayoutKind.Sequential)]
[NoReorder]
public readonly record struct Context
{
    internal readonly sockaddr_in serverAddress;

    /// <summary>The player's current map ID.</summary>
    public readonly uint MapId;

    /// <summary>The player's current map type.</summary>
    public readonly MapType MapType;

    /// <summary>The player's current shard ID.</summary>
    public readonly uint ShardId;

    /// <summary>The player's current instance.</summary>
    public readonly uint Instance;

    /// <summary>The build ID of the game client.</summary>
    public readonly uint BuildId;

    /// <summary>Miscellaneous user interface state flags.</summary>
    public readonly UiState UiState;

    /// <summary>The width of the resizable compass in pixels.</summary>
    public readonly ushort CompassWidth;

    /// <summary>The height of the resizable compass in pixels.</summary>
    public readonly ushort CompassHeight;

    /// <summary>The compass rotation in radians.</summary>
    public readonly float CompassRotation;

    /// <summary>The player's current X position in the continent coordinate system (inches).</summary>
    /// <remarks>This value can change by player movement. The value is not updated in competitive game types.</remarks>
    public readonly float PlayerX;

    /// <summary>The player's current Y position in the continent coordinate system (inches).</summary>
    /// <remarks>This value can change by player movement. The value is not updated in competitive game types.</remarks>
    public readonly float PlayerY;

    /// <summary>The current X position of the center of the world map or compass in the continent coordinate system (inches).</summary>
    /// <remarks>This value can change by player movement as the compass is centered on the player, or by interacting with the
    /// compass or world map to zoom and pan. The value is not updated in competitive game types</remarks>
    public readonly float MapCenterX;

    /// <summary>The current Y position of the center of the world map or compass in the continent coordinate system (inches).</summary>
    /// <remarks>This value can change by player movement as the compass is centered on the player, or by interacting with the
    /// compass or world map to zoom and pan. The value is not updated in competitive game types</remarks>
    public readonly float MapCenterY;

    /// <summary>The zoom level of the world map when the map is open, or the compass otherwise.</summary>
    public readonly float MapScale;

    /// <summary>The process ID of the game client.</summary>
    public readonly uint ProcessId;

    internal readonly byte MountIndex;

    /// <summary>Indicates whether the player is currently using a mount.</summary>
    [MemberNotNullWhen(true, nameof(Mount))]
    public bool IsMounted => MountIndex != 0;

    /// <summary>The server IP and port.</summary>
    public IPEndPoint ServerAddress =>
        new(serverAddress.sin_addr.s_un.s_addr, serverAddress.sin_port);

    /// <summary>The player's current mount.</summary>
    public MountName? Mount =>
        MountIndex switch
        {
            0 => null,
            1 => MountName.Jackal,
            2 => MountName.Griffon,
            3 => MountName.Springer,
            4 => MountName.Skimmer,
            5 => MountName.Raptor,
            6 => MountName.RollerBeetle,
            7 => MountName.Warclaw,
            8 => MountName.Skyscale,
            9 => MountName.Skiff,
            10 => MountName.SiegeTurtle,
            _ => throw new NotSupportedException("The current mount is not supported.")
        };
}
