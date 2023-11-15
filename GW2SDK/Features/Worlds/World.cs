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
    /// <see cref="WorldPopulation.Full" />. The transfer cost will be calculated based on the population: 500 gems for
    /// <see cref="WorldPopulation.Medium" />, 1000 gems for <see cref="WorldPopulation.High" /> and 1800 gems for
    /// <see cref="WorldPopulation.VeryHigh" />.</summary>
    public required WorldPopulation Population { get; init; }
}
