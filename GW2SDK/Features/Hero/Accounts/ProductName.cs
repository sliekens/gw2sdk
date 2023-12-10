using System.ComponentModel;

namespace GuildWars2.Hero.Accounts;

/// <summary>The names of Guild Wars 2 products.</summary>
[PublicAPI]
[DefaultValue(None)]
public enum ProductName
{
    /// <summary>Nothing. Zero, zilch, zip, nada, nothing.</summary>
    None,

    /// <summary>Indicates an account without any purchasable content.</summary>
    PlayForFree,

    /// <summary>The base game.</summary>
    GuildWars2,

    /// <summary>The Heart of Thorns (HoT) expansion.</summary>
    HeartOfThorns,

    /// <summary>The Path of Fire (PoF) expansion.</summary>
    PathOfFire,

    /// <summary>The End of Dragons (EoD) expansion.</summary>
    EndOfDragons
}
