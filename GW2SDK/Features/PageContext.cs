using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public sealed record PageContext(
        int ResultTotal,
        int ResultCount,
        int PageTotal,
        int PageSize,
        ContinuationToken First,
        ContinuationToken Self,
        ContinuationToken Last,
        ContinuationToken Previous,
        ContinuationToken Next
    ) : IPageContext;
}
