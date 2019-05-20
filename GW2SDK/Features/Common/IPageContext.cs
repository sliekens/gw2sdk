namespace GW2SDK.Features.Common
{
    public interface IPageContext : IListContext
    {
        int PageTotal { get; }

        int PageSize { get; }
    }
}
