namespace GuildWars2.Markup;

/// <summary>Represents a lexer for tokenizing markup input.</summary>
[PublicAPI]
public sealed class MarkupLexer
{
    private static readonly HashSet<string> VoidElements =
        new(StringComparer.OrdinalIgnoreCase) { "br" };

    /// <summary>Tokenizes the input string into a sequence of tokens.</summary>
    /// <param name="input">The markup input string to tokenize.</param>
    /// <returns>An <see cref="IEnumerable{Token}" /> representing the sequence of tokens parsed from the input string.</returns>
    public static IEnumerable<MarkupToken> Tokenize(string input)
    {
        // The language can be very roughly described by the following grammar:
        // MARKUP = *(TEXT / LF / TAG_OPEN / TAG_CLOSE / VOID_TAG)
        // TEXT = *VCHAR; except "<" and "\"
        // TAG_OPEN = "<" TAG_NAME [ "=" TAG_VALUE ] ">"
        // TAG_CLOSE = "</" TAG_NAME ">"
        // TAG_VOID = "<" TAG_NAME ">"
        // TAG_NAME = 1*ALPHA
        // TAG_VALUE = 1*VCHAR; except ">"
        ReadOnlyMemory<char> memory = input.AsMemory();
        var position = 0;
        var state = MarkupLexerState.Text;
        var start = 0;
        while (position < memory.Length)
        {
            var current = memory.Span[position];
            switch (state)
            {
                case MarkupLexerState.Text:
                    if (current == '<')
                    {
                        if (position > start)
                        {
                            yield return new MarkupToken(
                                MarkupTokenType.Text,
                                memory.Span[start..position].ToString()
                            );
                        }

                        state = MarkupLexerState.TagOpen;
                        start = position + 1;
                    }
                    else if (current == '\n')
                    {
                        if (position > start)
                        {
                            yield return new MarkupToken(
                                MarkupTokenType.Text,
                                memory.Span[start..position].ToString()
                            );
                        }

                        yield return new MarkupToken(MarkupTokenType.LineBreak, "");
                        state = MarkupLexerState.Text;
                        start = position + 1;
                    }

                    break;

                case MarkupLexerState.TagOpen:
                    if (current == '/')
                    {
                        if (position == start)
                        {
                            state = MarkupLexerState.TagClose;
                            start = position + 1;
                        }
                        else if (position + 1 < memory.Length && memory.Span[position + 1] == '>')
                        {
                            // Ignore the '/' in '/>'
                            var tagName = memory.Span[start..position].Trim().ToString();
                            if (VoidElements.Contains(tagName))
                            {
                                yield return new MarkupToken(MarkupTokenType.TagVoid, tagName);
                            }
                            else
                            {
                                yield return new MarkupToken(MarkupTokenType.TagStart, tagName);
                            }

                            state = MarkupLexerState.Text;
                            start = position + 2;
                        }
                        else
                        {
                            // Invalid tag
                            state = MarkupLexerState.Text;
                            start = position + 1;
                        }
                    }
                    else if (current == '=')
                    {
                        var tagName = memory.Span[start..position].Trim().ToString();
                        yield return new MarkupToken(MarkupTokenType.TagStart, tagName);
                        state = MarkupLexerState.TagValue;
                        start = position + 1;
                    }
                    else if (current == '>')
                    {
                        var tagName = memory.Span[start..position].Trim().ToString();
                        if (VoidElements.Contains(tagName))
                        {
                            yield return new MarkupToken(MarkupTokenType.TagVoid, tagName);
                        }
                        else
                        {
                            yield return new MarkupToken(MarkupTokenType.TagStart, tagName);
                        }

                        state = MarkupLexerState.Text;
                        start = position + 1;
                    }

                    break;

                case MarkupLexerState.TagValue:
                    if (current == '>')
                    {
                        var tagValue = memory.Span[start..position].Trim().ToString();
                        yield return new MarkupToken(MarkupTokenType.TagValue, tagValue);
                        state = MarkupLexerState.Text;
                        start = position + 1;
                    }

                    break;

                case MarkupLexerState.TagClose:
                    if (current == '>')
                    {
                        var tagName = memory.Span[start..position].Trim().ToString();
                        yield return new MarkupToken(MarkupTokenType.TagClose, tagName);
                        state = MarkupLexerState.Text;
                        start = position + 1;
                    }

                    break;
            }

            position++;
        }

        if (position > start)
        {
            yield return new MarkupToken(
                MarkupTokenType.Text,
                memory.Span[start..position].ToString()
            );
        }

        yield return new MarkupToken(MarkupTokenType.End, "");
    }
}
