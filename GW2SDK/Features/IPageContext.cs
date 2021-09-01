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

        HyperlinkReference Previous { get; }

        HyperlinkReference Next { get; }

        HyperlinkReference First { get; }

        HyperlinkReference Self { get; }

        HyperlinkReference Last { get; }
    }
}
