﻿using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming // because this is based on operating system APIs
namespace GuildWars2.Win32;

/// <summary>Union</summary>
[StructLayout(LayoutKind.Sequential)]
[UsedImplicitly(ImplicitUseTargetFlags.Members)]
internal readonly struct s_un_b
{
    internal readonly byte s_b1;

    internal readonly byte s_b2;

    internal readonly byte s_b3;

    internal readonly byte s_b4;
}
