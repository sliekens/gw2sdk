using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed record PageContext(
    int ResultTotal,
    int ResultCount,
    int PageTotal,
    int PageSize,
    HyperlinkReference First,
    HyperlinkReference Self,
    HyperlinkReference Last,
    HyperlinkReference Previous,
    HyperlinkReference Next
) : IPageContext;