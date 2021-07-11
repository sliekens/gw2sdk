using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IReplicaPage<T> : ITemporal
    {
        bool HasValues { get; }

        IReadOnlySet<T>? Values { get; }

        IPageContext? Context { get; }
    }
}
