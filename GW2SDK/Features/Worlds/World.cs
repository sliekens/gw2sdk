namespace GuildWars2.Worlds;

/// <summary>Information about a world (AKA server).</summary>
[PublicAPI]
[DataTransferObject]
public sealed record World
{
    /// <summary>The world ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the server.</summary>
    public required string Name { get; init; }

    /// <summary>The population of the server. Transferring to a different world is only possible when it is not
    /// <see cref="WorldPopulation.Full" />.</summary>
    /// <remarks>Exact player counts are unavailable.</remarks>
    public required Extensible<WorldPopulation> Population { get; init; }

    /// <summary>The gem fee for transferring to this world. These fees are based on the old WvW system based on home worlds,
    /// where the fee is based on the <see cref="Population" />. After the World Restructuring update, transferring to any
    /// world costs a flat 500 gems.</summary>
    public int TransferFee =>
        Population.ToEnum() switch
        {
            WorldPopulation.None => 0,
            WorldPopulation.Low => 0,
            WorldPopulation.Medium => 500,
            WorldPopulation.High => 1000,
            WorldPopulation.VeryHigh => 1800,
            WorldPopulation.Full => 0,
            _ => 0
        };

    /// <summary>Which region this world belongs to. This indicates the location of the data center that stores the account
    /// information. Players from one region cannot join players from another region. Only chat and mail are shared between
    /// regions.</summary>
    public WorldRegion Region =>
        Id switch
        {
            >= 1000 and < 2000 => WorldRegion.NorthAmerica,
            >= 2000 and < 3000 => WorldRegion.Europe,
            _ => WorldRegion.None
        };

    /// <summary>The language associated with this world.</summary>
    public Language Language =>
        (Id / 100 % 10) switch
        {
            1 => Language.French,
            2 => Language.German,
            3 => Language.Spanish,
            _ => Language.English
        };
}
