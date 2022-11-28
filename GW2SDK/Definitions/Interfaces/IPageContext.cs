using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
public interface IPageContext
{
    int PageTotal { get; }

    int PageSize { get; }

    int ResultTotal { get; }

    int ResultCount { get; }

    Hyperlink Previous { get; }

    Hyperlink Next { get; }

    Hyperlink First { get; }

    Hyperlink Self { get; }

    Hyperlink Last { get; }
}
