using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IReplica<out T> : ITemporal
    {
#if NET
        [MemberNotNullWhen(true, nameof(Value))]
        bool HasValue { get; }

        T? Value { get; }
#else
        bool HasValue { get; }

        T Value { get; }
#endif
    }
}
