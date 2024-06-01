namespace GuildWars2.Chat;

/// <summary>Represents an achievement chat link.</summary>
[PublicAPI]
public sealed record AchievementLink : Link
{
    /// <summary>The achievement ID.</summary>
    public required int AchievementId { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        var buffer = new LinkBuffer(stackalloc byte[5]);
        buffer.WriteUInt8(LinkHeader.Achievement);
        buffer.WriteInt32(AchievementId);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static AchievementLink Parse(string chatLink) => Parse(chatLink.AsSpan());

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static AchievementLink Parse(ReadOnlySpan<char> chatLink)
    {
        var bytes = GetBytes(chatLink);
        var buffer = new LinkBuffer(bytes);
        if (buffer.ReadUInt8() != LinkHeader.Achievement)
        {
            ThrowHelper.ThrowBadArgument("Expected an achievement chat link.", nameof(chatLink));
        }

        var achievementId = buffer.ReadInt32();
        return new AchievementLink { AchievementId = achievementId };
    }
}
