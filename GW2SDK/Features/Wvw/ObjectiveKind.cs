namespace GuildWars2.Wvw;

/// <summary>The kinds of objectives in World vs. World.</summary>
[PublicAPI]
public enum ObjectiveKind
{
    /// <summary>Camps.</summary>
    Camp = 1,

    /// <summary>Castles.</summary>
    Castle,

    /// <summary>Generic objectives in Edge of the Mists.</summary>
    Generic,

    /// <summary>Keeps.</summary>
    Keep,

    /// <summary>Mercenary camps.</summary>
    Mercenary,

    /// <summary>Resource objectives in Edge of the Mists.</summary>
    Resource,

    /// <summary>Ruins.</summary>
    Ruins,

    /// <summary>Spawn camps where players enter the map.</summary>
    Spawn,

    /// <summary>Towers.</summary>
    Tower
}
