using System.Runtime.InteropServices;

namespace GuildWars2.Mumble;

[StructLayout(LayoutKind.Sequential)]
public record struct Vector3D
{
    public readonly float X;

    public readonly float Y;

    public readonly float Z;
}
