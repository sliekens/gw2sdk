using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Logs;

/// <summary>A log entry about a member being invited to the guild.</summary>
[DataTransferObject]
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record MemberInvited : GuildLogEntry
{
    /// <summary>The ID of the user who was invited.</summary>
    public required string User { get; init; }

    /// <summary>The ID of the user who sent the invite.</summary>
    public required string InvitedBy { get; init; }
}
