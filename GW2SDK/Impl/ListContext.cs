namespace GW2SDK.Impl
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
