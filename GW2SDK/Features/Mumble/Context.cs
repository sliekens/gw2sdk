using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.InteropServices;
using GuildWars2.Win32;
using JetBrains.Annotations;

namespace GuildWars2.Mumble;

[PublicAPI]
[StructLayout(LayoutKind.Sequential)]
[NoReorder]
public readonly record struct Context
{
    internal readonly sockaddr_in serverAddress;

    public readonly uint MapId;

    public readonly MapType MapType;

    public readonly uint ShardId;

    public readonly uint Instance;

    public readonly uint BuildId;

    public readonly UiState UiState;

    public readonly ushort CompassWidth;

    public readonly ushort CompassHeight;

    public readonly float CompassRotation;

    public readonly float PlayerX;

    public readonly float PlayerY;

    public readonly float MapCenterX;

    public readonly float MapCenterY;

    public readonly float MapScale;

    public readonly uint ProcessId;

    internal readonly byte MountIndex;

    [MemberNotNullWhen(true, nameof(Mount))]
    public bool IsMounted => MountIndex != 0;

    public IPEndPoint ServerAddress =>
        new(serverAddress.sin_addr.s_un.s_addr, serverAddress.sin_port);

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
            10 => MountName.Turtle,
            _ => throw new NotSupportedException("The current mount is not supported.")
        };
}
