using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common
{
    [PublicAPI]
    public interface IPageContext
    {
        int PageTotal { get; }

        int PageSize { get; }
        
        int ResultTotal { get; }

        int ResultCount { get; }
    }
}
