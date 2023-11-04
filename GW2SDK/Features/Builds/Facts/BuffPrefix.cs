namespace GuildWars2.Builds.Facts;

/// <summary>A precondition for a <see cref="Buff" />. The effect is only applied when the precondition is active.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record BuffPrefix
{
    /// <summary>The buff effect.</summary>
    public required string Text { get; init; }

    /// <summary>The URL of the icon that appears in the tooltip before the actual buff's icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The status is a summary of the precondition. For example: "Fire Attunement" indicates the buff is applied if
    /// the skill is used while Fire Attunement is active.</summary>
    public required string Status { get; init; }

    /// <summary>The longer description of the precondition. For example: "Cast fire spells."</summary>
    public required string Description { get; init; }
}
