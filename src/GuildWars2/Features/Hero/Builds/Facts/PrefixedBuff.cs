namespace GuildWars2.Hero.Builds.Facts;

/// <summary>A boon, condition or effect with a precondition.</summary>
public sealed record PrefixedBuff : Buff
{
    /// <summary>The URL of the precondition's icon as it appears in the tooltip, before the <see cref="Fact.IconUrl" />.</summary>
    public required Uri PrefixIconUrl { get; init; }

    /// <summary>Indicates what is the precondition for this buff. For example: "Fire Attunement".</summary>
    public required string Precondition { get; init; }
}
