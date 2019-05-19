namespace GW2SDK.Features.Common
{
    public interface IPagedListMetaData : IListMetaData
    {
        int PageTotal { get; }

        int PageSize { get; }
    }
}
