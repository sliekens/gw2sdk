﻿using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming // because this is based on operating system APIs
#pragma warning disable IDE1006 // Naming Styles
namespace GW2SDK.Mumble.Win32
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct s_un // union
    {
        [FieldOffset(0)]
        internal readonly s_un_b s_un_b;

        [FieldOffset(0)]
        internal readonly s_un_w s_un_w;

        [FieldOffset(0)]
        internal readonly uint s_addr;
    }
}