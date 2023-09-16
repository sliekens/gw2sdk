using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace GuildWars2.Mumble;

[PublicAPI]
[StructLayout(LayoutKind.Sequential)]
public readonly record struct Vector3D
{
    public readonly float X;

    public readonly float Y;

    public readonly float Z;
}
