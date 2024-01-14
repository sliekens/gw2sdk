namespace GuildWars2.Hero.Builds;

/// <summary>Information about the selected pets for a Ranger build.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SelectedPets
{
    /// <summary>The ID of the first selected pet, or <c>null</c> if no pet was selected.</summary>
    public required int? Terrestrial1 { get; init; }

    /// <summary>The ID of the other selected pet, or <c>null</c> if no pet was selected.</summary>
    public required int? Terrestrial2 { get; init; }

    /// <summary>The ID of the first selected pet for underwater, or <c>null</c> if no pet was selected.</summary>
    public required int? Aquatic1 { get; init; }

    /// <summary>The ID of the other selected pet for underwater, or <c>null</c> if no pet was selected.</summary>
    public required int? Aquatic2 { get; init; }
}
