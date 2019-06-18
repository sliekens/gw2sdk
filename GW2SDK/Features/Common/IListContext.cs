using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common
{
    [PublicAPI]
    public interface IListContext
    {
        int ResultTotal { get; }

        int ResultCount { get; }
    }
}
