using GW2SDK.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface ICollectionContext
    {
        int ResultTotal { get; }

        int ResultCount { get; }
    }
}
