using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace GuildWars2.Win32;

/// <summary>Union</summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct s_un_b
{
    internal readonly byte s_b1;

    internal readonly byte s_b2;

    internal readonly byte s_b3;

    internal readonly byte s_b4;
}
