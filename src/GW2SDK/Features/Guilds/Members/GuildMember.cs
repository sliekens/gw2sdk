namespace GuildWars2.Guilds.Members;

/// <summary>Information about a member of a guild.</summary>
[DataTransferObject]
public sealed record GuildMember
{
    /// <summary>The user ID of the member.</summary>
    public required string Name { get; init; }

    /// <summary>The rank of the member.</summary>
    public required string Rank { get; init; }

    /// <summary>When the member joined the guild.</summary>
    /// <remarks>May also be null — the join date was not tracked before around March 19th, 2013.</remarks>
    public required DateTimeOffset? Joined { get; init; }

    /// <summary>Whether the member has selected the current guild for World vs. World.</summary>
    public required bool WvwMember { get; init; }
}
