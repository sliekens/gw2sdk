namespace GuildWars2.Hero.Accounts;

/// <summary>Modifiers for characters.</summary>
[PublicAPI]
public sealed record CharacterFlags : Flags
{
    /// <summary>Whether the character was created to participate in a beta event.</summary>
    public required bool Beta { get; init; }
}
