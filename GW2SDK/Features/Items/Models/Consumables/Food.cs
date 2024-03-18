namespace GuildWars2.Items;

/// <summary>Information about a food item, which typically grants a nourishment effect when consumed.</summary>
[PublicAPI]
public sealed record Food : Consumable
{
    /// <summary>The effect applied when the item is consumed.</summary>
    public required Effect? Effect { get; init; }
}
