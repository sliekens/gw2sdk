namespace GuildWars2.Authorization;

/// <summary>The result of creating a new subtoken.</summary>
[DataTransferObject]
public sealed record CreatedSubtoken
{
    /// <summary>The newly created access token.</summary>
    public required string Subtoken { get; init; }
}
