using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
public interface ICollectionContext
{
    int ResultTotal { get; }

    int ResultCount { get; }
}
