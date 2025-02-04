﻿namespace GuildWars2.Chat;

/// <summary>Represents a recipe chat link.</summary>
[PublicAPI]
public sealed record RecipeLink : Link
{
    /// <summary>The recipe ID.</summary>
    public required int RecipeId { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        var buffer = new LinkBuffer(stackalloc byte[5]);
        buffer.WriteUInt8(LinkHeader.Recipe);
        buffer.WriteInt32(RecipeId);
        return buffer.ToString();
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static RecipeLink Parse(string chatLink)
    {
        return Parse(chatLink.AsSpan());
    }

    /// <summary>Converts a chat link code to a chat link object.</summary>
    /// <param name="chatLink">The chat link text.</param>
    /// <returns>The chat link as an object.</returns>
    public static RecipeLink Parse(ReadOnlySpan<char> chatLink)
    {
        var bytes = GetBytes(chatLink);
        var buffer = new LinkBuffer(bytes);
        if (buffer.ReadUInt8() != LinkHeader.Recipe)
        {
            ThrowHelper.ThrowBadArgument("Expected a recipe chat link.", nameof(chatLink));
        }

        var recipeId = buffer.ReadInt32();
        return new RecipeLink { RecipeId = recipeId };
    }
}
