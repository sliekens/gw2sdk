namespace GW2SDK.Impl
{
    internal sealed class CollectionContext : ICollectionContext
    {
        internal CollectionContext(int resultTotal, int resultCount)
        {
            ResultTotal = resultTotal;
            ResultCount = resultCount;
        }

        public int ResultTotal { get; }

        public int ResultCount { get; }
    }
}
