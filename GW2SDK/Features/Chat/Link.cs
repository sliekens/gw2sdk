using System.Diagnostics;

namespace GuildWars2.Chat;

/// <summary>The base type for chat links.</summary>
[PublicAPI]
[DebuggerDisplay("{ToString(),nq}")]
public abstract record Link
{
    /// <summary>Converts the chat link object to a string that can be sent in chat.</summary>
    /// <returns>The chat link text.</returns>
    public abstract override string ToString();

    internal static Span<byte> GetBytes(string chatCode)
    {
        return GetBytes(chatCode.AsSpan());
    }

    internal static Span<byte> GetBytes(ReadOnlySpan<char> chatLink)
    {
        if (chatLink.IsEmpty || chatLink[0] != '[')
        {
            ThrowHelper.ThrowInvalidFormat("Invalid chat link format. Expected '['.");
        }

        chatLink = chatLink[1..];
        if (chatLink.IsEmpty || chatLink[0] != '&')
        {
            ThrowHelper.ThrowInvalidFormat("Invalid chat link format. Expected '&'.");
        }

        chatLink = chatLink[1..];
        if (chatLink.IsEmpty || chatLink[^1] != ']')
        {
            ThrowHelper.ThrowInvalidFormat("Invalid chat link format. Expected ']'.");
        }

        chatLink = chatLink[..^1];
        return Convert.FromBase64String(chatLink.ToString());
    }
}
