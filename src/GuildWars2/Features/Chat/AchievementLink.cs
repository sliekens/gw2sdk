namespace GuildWars2.Chat;

/// <summary>Represents an achievement chat link.</summary>
public sealed record AchievementLink : Link
{
    /// <summary>The achievement ID.</summary>
    public required int AchievementId { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        LinkBuffer buffer = new(stackalloc byte[5]);
        buffer.WriteUInt8(LinkHeader.Achievement);
        buffer.WriteInt32(AchievementId);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static AchievementLink Parse(string chatLink)
    {
        return Parse(chatLink.AsSpan());
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static AchievementLink Parse(in ReadOnlySpan<char> chatLink)
    {
        Span<byte> bytes = GetBytes(chatLink);
        LinkBuffer buffer = new(bytes);
        if (buffer.ReadUInt8() != LinkHeader.Achievement)
        {
            ThrowHelper.ThrowBadArgument("Expected an achievement chat link.", nameof(chatLink));
        }

        int achievementId = buffer.ReadInt32();
        return new AchievementLink { AchievementId = achievementId };
    }
}
