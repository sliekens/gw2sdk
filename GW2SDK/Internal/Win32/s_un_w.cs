using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace GuildWars2.Win32;

/// <summary>Union</summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct s_un_w
{
    internal readonly ushort s_w1;

    internal readonly ushort s_w2;
}
