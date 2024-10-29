
namespace GuildWars2.Markup;

/// <summary>
/// Represents a lexer for tokenizing markup input.
/// </summary>
[PublicAPI]
public sealed class MarkupLexer
{
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
        // The language can be very roughly described by the following grammar:
        // MARKUP = *(TEXT / LF / TAG_OPEN / TAG_CLOSE / VOID_TAG)
        // TEXT = *VCHAR; except "<" and "\"
        // TAG_OPEN = "<" TAG_NAME [ "=" TAG_VALUE ] ">"
        // TAG_CLOSE = "</" TAG_NAME ">"
        // TAG_VOID = "<" TAG_NAME ">"
        // TAG_NAME = 1*ALPHA
        // TAG_VALUE = 1*VCHAR; except ">"
        var scanner = new Scanner(input);
        var state = MarkupLexerState.Text;
        var start = scanner.Position;
        while (scanner.CanAdvance)
        {
            switch (state)
            {
                case MarkupLexerState.Text:
                    if (scanner.Current == '<')
                    {
                        if (scanner.Position > start)
                        {
                            yield return new MarkupToken(MarkupTokenType.Text, input[start..scanner.Position]);
                        }

                        state = MarkupLexerState.TagOpen;
                        start = scanner.Position + 1;
                    }
                    else if (scanner.Current == '\n')
                    {
                        if (scanner.Position > start)
                        {
                            yield return new MarkupToken(MarkupTokenType.Text, input[start..scanner.Position]);
                        }

                        yield return new MarkupToken(MarkupTokenType.LineBreak, "");
                        state = MarkupLexerState.Text;
                        start = scanner.Position + 1;
                    }

                    break;

                case MarkupLexerState.TagOpen:
                    if (scanner.Current == '/')
                    {
                        if (scanner.Position == start)
                        {
                            state = MarkupLexerState.TagClose;
                            start = scanner.Position + 1;
                        }
                        else if (scanner.Peek() == '>')
                        {
                            // Ignore the '/' in '/>'
                            var tagName = input[start..scanner.Position].Trim();
                            if (VoidElements.Contains(tagName))
                            {
                                yield return new MarkupToken(MarkupTokenType.TagVoid, tagName);
                            }
                            else
                            {
                                yield return new MarkupToken(MarkupTokenType.TagStart, tagName);
                            }

                            state = MarkupLexerState.Text;
                            start = scanner.Position + 2;
                        }
                        else
                        {
                            // Invalid tag
                            state = MarkupLexerState.Text;
                            start = scanner.Position + 1;
                        }
                    }
                    else if (scanner.Current == '=')
                    {
                        var tagName = input[start..scanner.Position].Trim();
                        yield return new MarkupToken(MarkupTokenType.TagStart, tagName);
                        state = MarkupLexerState.TagValue;
                        start = scanner.Position + 1;
                    }
                    else if (scanner.Current == '>')
                    {
                        var tagName = input[start..scanner.Position].Trim();
                        if (VoidElements.Contains(tagName))
                        {
                            yield return new MarkupToken(MarkupTokenType.TagVoid, tagName);
                        }
                        else
                        {
                            yield return new MarkupToken(MarkupTokenType.TagStart, tagName);
                        }

                        state = MarkupLexerState.Text;
                        start = scanner.Position + 1;
                    }

                    break;

                case MarkupLexerState.TagValue:
                    if (scanner.Current == '>')
                    {
                        var tagValue = input[start..scanner.Position].Trim();
                        yield return new MarkupToken(MarkupTokenType.TagValue, tagValue);
                        state = MarkupLexerState.Text;
                        start = scanner.Position + 1;
                    }

                    break;

                case MarkupLexerState.TagClose:
                    if (scanner.Current == '>')
                    {
                        var tagName = input[start..scanner.Position].Trim();
                        yield return new MarkupToken(MarkupTokenType.TagClose, tagName);
                        state = MarkupLexerState.Text;
                        start = scanner.Position + 1;
                    }

                    break;
            }

            scanner.Advance();
        }

        if (scanner.Position > start)
        {
            yield return new MarkupToken(MarkupTokenType.Text, input[start..scanner.Position]);
        }

        yield return new MarkupToken(MarkupTokenType.End, "");
    }
}
