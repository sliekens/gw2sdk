﻿namespace GuildWars2.Chat;

/// <summary>Represents a translation chat link.</summary>
[PublicAPI]
public sealed record TranslationLink : Link
{
    /// <summary>The translation ID.</summary>
    public required int TranslationId { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        LinkBuffer buffer = new(stackalloc byte[5]);
        buffer.WriteUInt8(LinkHeader.Translation);
        buffer.WriteInt32(TranslationId);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static TranslationLink Parse(string chatLink)
    {
        return Parse(chatLink.AsSpan());
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static TranslationLink Parse(in ReadOnlySpan<char> chatLink)
    {
        var bytes = GetBytes(chatLink);
        LinkBuffer buffer = new(bytes);
        if (buffer.ReadUInt8() != LinkHeader.Translation)
        {
            ThrowHelper.ThrowBadArgument("Expected a translation chat link.", nameof(chatLink));
        }

        var translationId = buffer.ReadInt32();
        return new TranslationLink { TranslationId = translationId };
    }
}
