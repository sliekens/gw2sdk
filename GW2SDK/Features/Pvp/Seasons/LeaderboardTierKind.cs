using System.ComponentModel;

namespace GuildWars2.Pvp.Seasons;

/// <summary>The method used to calculate the tier of a player or team on a PvP League leaderboard.</summary>
[PublicAPI]
[DefaultValue(Rank)]
public enum LeaderboardTierKind
{
    /// <summary>The player or team rank is used to calculate the tier.</summary>
    Rank
}
