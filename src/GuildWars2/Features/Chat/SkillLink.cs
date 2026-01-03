namespace GuildWars2.Chat;

/// <summary>Represents a skill chat link.</summary>
public sealed record SkillLink : Link
{
    /// <summary>The skill ID.</summary>
    public required int SkillId { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        LinkBuffer buffer = new(stackalloc byte[5]);
        buffer.WriteUInt8(LinkHeader.Skill);
        buffer.WriteInt32(SkillId);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static SkillLink Parse(string chatLink)
    {
        return Parse(chatLink.AsSpan());
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static SkillLink Parse(in ReadOnlySpan<char> chatLink)
    {
        Span<byte> bytes = GetBytes(chatLink);
        LinkBuffer buffer = new(bytes);
        if (buffer.ReadUInt8() != LinkHeader.Skill)
        {
            ThrowHelper.ThrowBadArgument("Expected a skill chat link.", nameof(chatLink));
        }

        int skillId = buffer.ReadInt32();
        return new SkillLink { SkillId = skillId };
    }
}
