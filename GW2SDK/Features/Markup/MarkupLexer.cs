
namespace GuildWars2.Markup;

/// <summary>
/// Represents a lexer for tokenizing markup input.
/// </summary>
[PublicAPI]
public sealed class MarkupLexer(string input)
{
    private static readonly char[] EqualSign = ['='];

    private static readonly HashSet<string> VoidElements = new(StringComparer.OrdinalIgnoreCase)
    {
        "br"
    };

    private int position;

    private char Current => position >= input.Length ? '\0' : input[position];

    private void Advance() => position++;

    private string ReadUntil(char c)
    {
        var start = position;
        while (Current != c && position < input.Length)
        {
            Advance();
        }

        return input.Substring(start, position - start);
    }

    /// <summary>
    /// Tokenizes the input string into a sequence of tokens.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{Token}"/> representing the sequence of tokens parsed from the input string.
    /// </returns>
    public IEnumerable<MarkupToken> Tokenize()
    {
        while (position < input.Length)
        {
            if (Current == '<')
            {
                Advance();
                if (Current == '/')
                {
                    Advance();
                    var tagName = ReadUntil('>');
                    Advance();
                    yield return new MarkupToken(MarkupTokenType.TagClose, tagName);
                }
                else
                {
                    var tagName = ReadUntil('>');
                    Advance();
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
                var text = ReadUntil('<');
                yield return new MarkupToken(MarkupTokenType.Text, text);
            }
        }

        yield return new MarkupToken(MarkupTokenType.End, "");
    }
}
