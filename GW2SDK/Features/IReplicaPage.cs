﻿using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    // ReSharper disable once TypeParameterCanBeVariant // it's a lie
    public interface IReplicaPage<T> : ITemporal
    {
#if NET
        [System.Diagnostics.CodeAnalysis.MemberNotNullWhen(true, nameof(Values), nameof(Context))]
        bool HasValues { get; }

        IReadOnlySet<T>? Values { get; }

        IPageContext? Context { get; }
#else
        bool HasValues { get; }

        IReadOnlyCollection<T> Values { get; }

        IPageContext Context { get; }
#endif
    }
}
