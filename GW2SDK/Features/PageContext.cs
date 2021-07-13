namespace GW2SDK
{
    internal sealed record PageContext(
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
