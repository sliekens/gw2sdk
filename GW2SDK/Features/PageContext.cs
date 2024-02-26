namespace GuildWars2;

/// <summary>Represents the context of a page of entities.</summary>
/// <param name="PageTotal">The total number of pages.</param>
/// <param name="PageSize">The number of items per page.</param>
/// <param name="First">The hyperlink to the first page.</param>
/// <param name="Self">The hyperlink to the current page.</param>
/// <param name="Last">The hyperlink to the last page.</param>
/// <param name="Previous">The hyperlink to the previous page.</param>
/// <param name="Next">The hyperlink to the next page.</param>
[PublicAPI]
public sealed record PageContext(
    int PageTotal,
    int PageSize,
    Hyperlink First,
    Hyperlink Self,
    Hyperlink Last,
    Hyperlink Previous,
    Hyperlink Next
);
