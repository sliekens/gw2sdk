using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IDataTransferSet<T> : ITemporal
    {
        [MemberNotNullWhen(true, nameof(Values), nameof(Context))]
        bool HasValues { get; }

        IReadOnlySet<T>? Values { get; }

        ICollectionContext? Context { get; }
    }
}
