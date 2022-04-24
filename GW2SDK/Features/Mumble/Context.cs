using System;
using System.Net;
using System.Runtime.InteropServices;
using GW2SDK.Mumble.Win32;
using JetBrains.Annotations;

namespace GW2SDK.Mumble;

[PublicAPI]
[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
public struct Context
{
    [FieldOffset(0)]
    internal readonly sockaddr_in ServerAddress;

    [FieldOffset(28)]
    public readonly uint MapId;

    [FieldOffset(32)]
    public readonly uint MapType;

    [FieldOffset(36)]
    public readonly uint ShardId;

    [FieldOffset(40)]
    public readonly uint Instance;

    [FieldOffset(44)]
    public readonly uint BuildId;

    [FieldOffset(48)]
    public readonly UiState UiState;

    [FieldOffset(52)]
    public readonly ushort CompassWidth;

    [FieldOffset(54)]
    public readonly ushort CompassHeight;

    [FieldOffset(56)]
    public readonly float CompassRotation;

    [FieldOffset(60)]
    public readonly float PlayerX;

    [FieldOffset(64)]
    public readonly float PlayerY;

    [FieldOffset(68)]
    public readonly float MapCenterX;

    [FieldOffset(72)]
    public readonly float MapCenterY;

    [FieldOffset(76)]
    public readonly float MapScale;

    [FieldOffset(80)]
    public readonly uint ProcessId;

    [FieldOffset(84)]
    internal readonly byte MountIndex;

    public bool IsMounted => MountIndex != 0;

    public IPEndPoint GetServerAddress() =>
        new(ServerAddress.sin_addr.s_un.s_addr, ServerAddress.sin_port);

    public MountName GetMount() =>
        MountIndex switch
        {
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
