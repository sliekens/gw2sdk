namespace GuildWars2.Builds.Facts;

/// <summary>A boon, condition or effect applied (or removed) by the skill/trait when a precondition is met.</summary>
[PublicAPI]
public sealed record PrefixedBuff : Buff
{
    /// <summary>The precondition for this buff.</summary>
    public required BuffPrefix Prefix { get; init; }
}
