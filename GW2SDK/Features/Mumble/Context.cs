using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.InteropServices;
using GuildWars2.Win32;
using JetBrains.Annotations;

namespace GuildWars2.Mumble;

[PublicAPI]
[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
public record struct Context
{
    [FieldOffset(0)]
    internal readonly sockaddr_in serverAddress;

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

    [MemberNotNullWhen(true, nameof(Mount))]
    public readonly bool IsMounted => MountIndex != 0;

    public readonly IPEndPoint ServerAddress =>
        new(serverAddress.sin_addr.s_un.s_addr, serverAddress.sin_port);

    public readonly MountName? Mount =>
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
