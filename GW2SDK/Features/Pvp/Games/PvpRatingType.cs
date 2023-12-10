using System.ComponentModel;

namespace GuildWars2.Pvp.Games;

[PublicAPI]
[DefaultValue(None)]
public enum PvpRatingType
{
    None,

    Ranked,

    Ranked2v2,

    Ranked3v3,

    Unranked,

    Placeholder
}
