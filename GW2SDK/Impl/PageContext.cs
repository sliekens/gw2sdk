namespace GW2SDK.Impl
{
    internal sealed class PageContext : IPageContext
    {
        internal PageContext(int resultTotal, int resultCount, int pageTotal, int pageSize)
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
