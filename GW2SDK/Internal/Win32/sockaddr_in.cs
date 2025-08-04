using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace GuildWars2.Win32;

[StructLayout(LayoutKind.Sequential, Size = 28)]
internal readonly struct sockaddr_in
{
    internal readonly short sin_family;

    internal readonly ushort sin_port;

    internal readonly in_addr sin_addr;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
    internal readonly string sin_zero;
}
