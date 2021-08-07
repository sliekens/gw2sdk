using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming // because this is based on operating system APIs

namespace GW2SDK.Mumble.Cpp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct sockaddr_in
    {
        public short sin_family;

        public ushort sin_port;

        public in_addr sin_addr;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string sin_zero;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct in_addr
    {
        public s_un s_un;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct s_un // union
    {
        [FieldOffset(0)]
        public s_un_b s_un_b;

        [FieldOffset(0)]
        public s_un_w s_un_w;

        [FieldOffset(0)]
        public uint s_addr;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct s_un_b // union
    {
        public byte s_b1;

        public byte s_b2;

        public byte s_b3;

        public byte s_b4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct s_un_w // union
    {
        public ushort s_w1;

        public ushort s_w2;
    }
}
