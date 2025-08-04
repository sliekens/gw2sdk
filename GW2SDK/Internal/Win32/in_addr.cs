using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace GuildWars2.Win32;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct in_addr
{
    internal readonly s_un s_un;
}
