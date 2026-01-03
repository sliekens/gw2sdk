namespace GuildWars2.Pve.Home.Decorations;

/// <summary>Information about a homestead decoration category.</summary>
[DataTransferObject]
public sealed record DecorationCategory
{
    /// <summary>The decoration category ID.</summary>
    public required int Id { get; init; }

    /// <summary>The decoration category name.</summary>
    public required string Name { get; init; }
}
