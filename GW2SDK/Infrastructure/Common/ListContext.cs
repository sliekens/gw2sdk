using GW2SDK.Features.Common;

namespace GW2SDK.Infrastructure.Common
{
    public sealed class ListContext : IListContext
    {
        public ListContext(int resultTotal, int resultCount)
        {
            ResultTotal = resultTotal;
            ResultCount = resultCount;
        }

        public int ResultTotal { get; }

        public int ResultCount { get; }
    }
}
