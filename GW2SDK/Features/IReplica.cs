using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IReplica<out T> : ITemporal
    {
        bool HasValue { get; }

        T? Value { get; }
    }
}
