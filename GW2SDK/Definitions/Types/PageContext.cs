using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed record PageContext(
    int ResultTotal,
    int ResultCount,
    int PageTotal,
    int PageSize,
    Hyperlink First,
    Hyperlink Self,
    Hyperlink Last,
    Hyperlink Previous,
    Hyperlink Next
) : IPageContext;
