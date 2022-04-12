using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public interface IReplica<out T> : ITemporal
{
    T Value { get; }
}