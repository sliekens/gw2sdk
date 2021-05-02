namespace GW2SDK
{
    internal sealed class PageContext : IPageContext
    {
        internal PageContext(
            int resultTotal,
            int resultCount,
            int pageTotal,
            int pageSize,
            ContinuationToken first,
            ContinuationToken self,
            ContinuationToken last,
            ContinuationToken? previous,
            ContinuationToken? next)
        {
            ResultTotal = resultTotal;
            ResultCount = resultCount;
            PageTotal = pageTotal;
            PageSize = pageSize;
            First = first;
            Self = self;
            Last = last;
            Previous = previous;
            Next = next;
        }

        public int ResultTotal { get; }

        public int ResultCount { get; }

        public int PageTotal { get; }

        public int PageSize { get; }

        public ContinuationToken? Previous { get; }

        public ContinuationToken? Next { get; }

        public ContinuationToken First { get; }

        public ContinuationToken Self { get; }

        public ContinuationToken Last { get; }
    }
}
