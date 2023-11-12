﻿using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming // because this is based on operating system APIs
namespace GuildWars2.Win32;

[StructLayout(LayoutKind.Sequential)]
[UsedImplicitly(ImplicitUseTargetFlags.Members)]
internal readonly struct s_un_w // union
{
    internal readonly ushort s_w1;

    internal readonly ushort s_w2;
}
