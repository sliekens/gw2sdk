using System.Collections.Generic;

using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]

// ReSharper disable once TypeParameterCanBeVariant // it's a lie
public interface IReplicaPage<T> : IReadOnlyCollection<T>, ITemporal
{
#if NET
    IReadOnlySet<T> Values { get; }
#else
    IReadOnlyCollection<T> Values { get; }

#endif
    IPageContext Context { get; }
}