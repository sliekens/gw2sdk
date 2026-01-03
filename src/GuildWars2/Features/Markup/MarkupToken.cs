namespace GuildWars2.Markup;

/// <summary>Represents a token with a specific type and value.</summary>
/// <param name="Type">The type of the token.</param>
/// <param name="Value">The value of the token.</param>
/// <returns>A string representation of the token in the format "Type: Value".</returns>
public sealed record MarkupToken(MarkupTokenType Type, string Value)
{
    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Type,-15}: {Value}";
    }
}
