namespace GW2SDK.Features.Common
{
    public interface IPageContext
    {
        int PageTotal { get; }

        int PageSize { get; }
        
        int ResultTotal { get; }

        int ResultCount { get; }
    }
}
