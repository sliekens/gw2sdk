using System;
using System.Net;
using System.Runtime.InteropServices;
using GW2SDK.Mumble.Cpp;
using JetBrains.Annotations;

namespace GW2SDK.Mumble
{
    [PublicAPI]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Snapshot
    {
        public uint UiVersion;

        public uint UiTick;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fAvatarPosition;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fAvatarFront;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fAvatarTop;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fCameraPosition;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fCameraFront;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fCameraTop;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string identity;

        /// <summary>Despite the actual context containing more data, this value is always 48. This field tells Mumble to use the
        /// first 48 bytes to uniquely identify people on the same server shards. The remaining 208 bytes are purely informational.</summary>
        public uint context_len;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] context;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string description;

        public Context GetContext()
        {
            if (name != "Guild Wars 2")
            {
                return default;
            }

            var handle = GCHandle.Alloc(context, GCHandleType.Pinned);
            try
            {
                var addr = handle.AddrOfPinnedObject();
                return Marshal.PtrToStructure<Context>(addr);
            }
            finally
            {
                handle.Free();
            }
        }
    }

    [PublicAPI]
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct Context
    {
        [FieldOffset(0)]
        public sockaddr_in ServerAddress;
        
        [FieldOffset(28)]
        public uint MapId;
        
        [FieldOffset(32)]
        public uint MapType;
        
        [FieldOffset(36)]
        public uint ShardId;
        
        [FieldOffset(40)]
        public uint Instance;
        
        [FieldOffset(44)]
        public uint BuildId;

        [FieldOffset(48)]
        public UiState UiState;

        [FieldOffset(52)]
        public ushort CompassWidth;

        [FieldOffset(54)]
        public ushort CompassHeight;

        [FieldOffset(56)]
        public float CompassRotation;

        [FieldOffset(60)]
        public float PlayerX;

        [FieldOffset(64)]
        public float PlayerY;

        [FieldOffset(68)]
        public float MapCenterX;

        [FieldOffset(72)]
        public float MapCenterY;

        [FieldOffset(76)]
        public float MapScale;

        [FieldOffset(80)]
        public uint ProcessId;

        [FieldOffset(84)]
        public byte MountIndex;

        public bool IsMounted() => MountIndex != 0;

        public IPEndPoint GetServerAddress() => new(ServerAddress.sin_addr.s_un.s_addr, ServerAddress.sin_port);

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
                _ => throw new NotSupportedException("The current mount is not supported.")
            };
    }

    [Flags]
    public enum UiState : uint
    {
        None,

        IsMapOpen = 0b_1,

        IsCompassTopRight = 0b_10,

        DoesCompassHaveRotationEnabled = 0b_100,

        GameHasFocus = 0b_1000,

        IsInCompetitiveGameMode = 0b_1_0000,

        TextboxHasFocus = 0b_10_0000,

        IsInCombat = 0b_100_0000
    }
}
