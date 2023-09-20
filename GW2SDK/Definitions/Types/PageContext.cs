namespace GuildWars2;

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
