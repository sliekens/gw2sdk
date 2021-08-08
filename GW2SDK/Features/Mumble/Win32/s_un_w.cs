using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming // because this is based on operating system APIs
#pragma warning disable IDE1006 // Naming Styles
namespace GW2SDK.Mumble.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct s_un_w // union
    {
        internal readonly ushort s_w1;

        internal readonly ushort s_w2;
    }
}