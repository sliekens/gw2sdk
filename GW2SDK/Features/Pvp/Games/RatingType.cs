using System.ComponentModel;

namespace GuildWars2.Pvp.Games;

[PublicAPI]
[DefaultValue(None)]
public enum RatingType
{
    None,
    Ranked,
    Unranked,
    Placeholder
}
