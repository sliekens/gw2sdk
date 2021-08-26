﻿using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming // because this is based on operating system APIs
#pragma warning disable IDE1006 // Naming Styles
namespace GW2SDK.Mumble.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct s_un_b // union
    {
        internal readonly byte s_b1;

        internal readonly byte s_b2;

        internal readonly byte s_b3;

        internal readonly byte s_b4;
    }
}