﻿using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming // because this is based on operating system APIs
namespace GuildWars2.Win32;

[StructLayout(LayoutKind.Sequential)]
[UsedImplicitly(ImplicitUseTargetFlags.Members)]
internal readonly struct in_addr
{
    internal readonly s_un s_un;
}
