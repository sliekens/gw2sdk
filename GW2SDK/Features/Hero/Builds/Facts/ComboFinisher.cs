namespace GuildWars2.Hero.Builds.Facts;

/// <summary>A combo finisher effect activated by the skill/trait.</summary>
[PublicAPI]
public sealed record ComboFinisher : Fact
{
    /// <summary>The chance of activating the combo finisher effect. Expressed as a percentage, where 100 is 100% chance.</summary>
    public required int Percent { get; init; }

    /// <summary>The kind of combo finisher effect applied by the skill.</summary>
    public required ComboFinisherName FinisherName { get; init; }
}
