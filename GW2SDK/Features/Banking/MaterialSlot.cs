namespace GuildWars2.Banking;

/// <summary>Information about a material that can be stored in material storage.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record MaterialSlot
{
    /// <summary>The item ID of the material.</summary>
    public required int ItemId { get; init; }

    /// <summary>The material category ID which can be used to look up the category name.</summary>
    public required int CategoryId { get; init; }

    /// <summary>The binding of the material. Either <see cref="ItemBinding.Account" /> or <see cref="ItemBinding.None" />.</summary>
    public required ItemBinding Binding { get; init; }

    /// <summary>How many of the material are stored in the account's material storage.</summary>
    public required int Count { get; init; }
}
