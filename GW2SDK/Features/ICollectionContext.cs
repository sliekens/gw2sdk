using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public interface ICollectionContext
{
    int ResultTotal { get; }

    int ResultCount { get; }
}
