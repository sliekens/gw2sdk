namespace GuildWars2.Hero.Wallet;

/// <summary>Information about a currency in the account wallet.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Currency
{
    /// <summary>The currency ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the currency.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the currency as displayed in the wallet panel.</summary>
    public required string Description { get; init; }

    /// <summary>The display order of the currency in the wallet panel.</summary>
    public required int Order { get; init; }

    /// <summary>The URL of the currency icon.</summary>
    public required string IconHref { get; init; }
}
