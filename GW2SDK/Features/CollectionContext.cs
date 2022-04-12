using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed record CollectionContext(int ResultTotal, int ResultCount) : ICollectionContext;