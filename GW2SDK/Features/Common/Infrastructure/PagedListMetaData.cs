namespace GW2SDK.Features.Common.Infrastructure
{
    public sealed class PagedListMetaData : IPagedListMetaData
    {
        public int ResultTotal { get; internal set; }

        public int ResultCount { get; internal set; }

        public int PageTotal { get; internal set; }

        public int PageSize { get; internal set; }
    }
}