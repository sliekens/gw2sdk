using System.Runtime.InteropServices;

namespace GuildWars2.Win32;

#pragma warning disable IDE1006 // Naming Styles

/// <summary>Union</summary>
[StructLayout(LayoutKind.Explicit)]
internal readonly struct s_un
{
    [FieldOffset(0)]
    internal readonly s_un_b s_un_b;

    [FieldOffset(0)]
    internal readonly s_un_w s_un_w;

    [FieldOffset(0)]
    internal readonly uint s_addr;
}
