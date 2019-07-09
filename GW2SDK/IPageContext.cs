using GW2SDK.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IPageContext
    {
        int PageTotal { get; }

        int PageSize { get; }
        
        int ResultTotal { get; }

        int ResultCount { get; }
    }
}
