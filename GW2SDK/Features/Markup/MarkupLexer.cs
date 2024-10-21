
namespace GuildWars2.Markup;

/// <summary>
/// Represents a lexer for tokenizing markup input.
/// </summary>
[PublicAPI]
public sealed class MarkupLexer
{
    private static readonly char[] EqualSign = ['='];

    private static readonly HashSet<string> VoidElements = new(StringComparer.OrdinalIgnoreCase)
    {
        "br"
    };

    /// <summary>
    /// Tokenizes the input string into a sequence of tokens.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{Token}"/> representing the sequence of tokens parsed from the input string.
    /// </returns>
    public IEnumerable<MarkupToken> Tokenize(string input)
    {
        var scanner = new Scanner(input);
        while (scanner.CanAdvance)
        {
            if (scanner.Current == '<')
            {
                scanner.Advance();
                if (scanner.Current == '/')
                {
                    scanner.Advance();
                    var tagName = scanner.ReadUntil('>');
                    scanner.Advance();
                    yield return new MarkupToken(MarkupTokenType.TagClose, tagName);
                }
                else
                {
                    var tagName = scanner.ReadUntil('>');
                    scanner.Advance();
                    if (VoidElements.Contains(tagName))
                    {
                        yield return new MarkupToken(MarkupTokenType.TagVoid, tagName);
                    }
                    else if (tagName.Contains('='))
                    {
                        var parts = tagName.Split(EqualSign, 2);
                        yield return new MarkupToken(MarkupTokenType.TagStart, parts[0]);
                        yield return new MarkupToken(MarkupTokenType.TagValue, parts[1]);
                    }
                    else
                    {
                        yield return new MarkupToken(MarkupTokenType.TagStart, tagName);
                    }
                }
            }
            else
            {
                var text = scanner.ReadUntil('<');
                yield return new MarkupToken(MarkupTokenType.Text, text);
            }
        }

        yield return new MarkupToken(MarkupTokenType.End, "");
    }
}
