using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IReplicaSet<T> : ITemporal
    {
        bool HasValues { get; }

        IReadOnlySet<T>? Values { get; }

        ICollectionContext? Context { get; }
    }
}
