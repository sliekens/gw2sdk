namespace GuildWars2;

[PublicAPI]
public sealed record CharacterFlags
{
    /// <summary>Whether the character was created to participate in a beta event.</summary>
    public required bool Beta { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
