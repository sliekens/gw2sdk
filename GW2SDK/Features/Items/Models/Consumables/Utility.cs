namespace GuildWars2.Items;

/// <summary>Information about a utility consumable, which typically grants an enhancement effect when consumed.</summary>
[PublicAPI]
public sealed record Utility : Consumable
{
    /// <summary>The effect applied when the item is consumed.</summary>
    public required Effect? Effect { get; init; }
}
