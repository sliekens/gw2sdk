using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
public sealed record CollectionContext(int ResultTotal, int ResultCount) : ICollectionContext;
