using System.Collections.Generic;
using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]

// ReSharper disable once TypeParameterCanBeVariant // it's a lie
public interface IReplicaSet<T> : IReadOnlyCollection<T>, ITemporal
{
    IReadOnlyCollection<T> Values { get; }

    ICollectionContext Context { get; }
}
