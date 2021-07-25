using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IReplicaPage<T> : ITemporal
    {
#if NET
        [MemberNotNullWhen(true, nameof(Values), nameof(Context))]
        bool HasValues { get; }

        IReadOnlySet<T>? Values { get; }

        IPageContext? Context { get; }
#else
        bool HasValues { get; }

        ISet<T> Values { get; }

        IPageContext Context { get; }
#endif
    }
}
