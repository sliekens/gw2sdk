using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IDataTransfer<T> : ITemporal
    {
        [MemberNotNullWhen(true, nameof(Value))]
        bool HasValue { get; }

        T? Value { get; }
    }
}
