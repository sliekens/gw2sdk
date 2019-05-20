using GW2SDK.Features.Common;

namespace GW2SDK.Infrastructure.Common
{
    public sealed class PageContext : IPageContext
    {
        public PageContext(int resultTotal, int resultCount, int pageTotal, int pageSize)
        {
            ResultTotal = resultTotal;
            ResultCount = resultCount;
            PageTotal = pageTotal;
            PageSize = pageSize;
        }

        public int ResultTotal { get; }

        public int ResultCount { get; }

        public int PageTotal { get; }

        public int PageSize { get; }
    }
}
