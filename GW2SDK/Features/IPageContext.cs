using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IPageContext
    {
        int PageTotal { get; }

        int PageSize { get; }

        int ResultTotal { get; }

        int ResultCount { get; }

        ContinuationToken Previous { get; }

        ContinuationToken Next { get; }

        ContinuationToken First { get; }

        ContinuationToken Self { get; }

        ContinuationToken Last { get; }
    }
}
