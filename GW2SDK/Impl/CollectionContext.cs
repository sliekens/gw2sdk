namespace GW2SDK.Impl
{
    public sealed class CollectionContext : ICollectionContext
    {
        public CollectionContext(int resultTotal, int resultCount)
        {
            ResultTotal = resultTotal;
            ResultCount = resultCount;
        }

        public int ResultTotal { get; }

        public int ResultCount { get; }
    }
}
