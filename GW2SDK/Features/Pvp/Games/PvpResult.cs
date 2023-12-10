namespace GuildWars2.Pvp.Games;

/// <summary>The result of a PvP match.</summary>
[PublicAPI]
public enum PvpResult
{
    /// <summary>Your team won.</summary>
    Victory = 1,

    /// <summary>Your team lost.</summary>
    Defeat,

    /// <summary>The other team forfeited.</summary>
    Bye,

    /// <summary>Your team forfeited.</summary>
    Forfeit,

    /// <summary>You deserted your team.</summary>
    Desertion
}
