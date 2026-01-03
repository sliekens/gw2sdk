namespace GuildWars2.Guilds.Permissions;

/// <summary>Information about a guild permission.</summary>
[DataTransferObject]
public sealed record GuildPermissionSummary
{
    /// <summary>The permission ID.</summary>
    public required string Id { get; init; }

    /// <summary>The name of the permission.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the permission.</summary>
    public required string Description { get; init; }
}
