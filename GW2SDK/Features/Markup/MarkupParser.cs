using System.Diagnostics;

namespace GuildWars2.Markup;

/// <summary>
/// Represents a parser that converts a sequence of tokens into a hierarchical node structure.
/// </summary>
[PublicAPI]
public sealed class MarkupParser
{
    /// <summary>
    /// Parses a sequence of tokens into a hierarchical node structure.
    /// </summary>
    /// <returns>The root node of the parsed structure.</returns>
    public RootNode Parse(IEnumerable<MarkupToken> input)
    {
        var iterator = new MarkupTokenIterator(input);
        var root = new RootNode();
        while (iterator.Current is { Type: not MarkupTokenType.End })
        {
            var node = ParseNode(iterator);
            if (node is not null)
            {
                root.Children.Add(node);
            }
        }

        return root;
    }

    private static MarkupNode? ParseNode(MarkupTokenIterator iterator)
    {
        switch (iterator.Current?.Type)
        {
            case MarkupTokenType.Text:
                return ParseTextNode(iterator);
            case MarkupTokenType.TagStart:
                return ParseTagNode(iterator);
            case MarkupTokenType.TagVoid:
                return ParseVoidNode(iterator);
            default:
                iterator.Advance();
                return null;
        };
    }

    private static MarkupNode? ParseVoidNode(MarkupTokenIterator iterator)
    {
        Debug.Assert(iterator.Current?.Type == MarkupTokenType.TagVoid);
        var tagName = iterator.Current!.Value;
        iterator.Advance();

        if (string.Equals(tagName, "br", StringComparison.OrdinalIgnoreCase))
        {
            return new LineBreakNode();
        }

        return null;
    }

    private static MarkupNode? ParseTagNode(MarkupTokenIterator iterator)
    {
        Debug.Assert(iterator.Current?.Type == MarkupTokenType.TagStart);
        var tagName = iterator.Current!.Value;
        iterator.Advance();

        if (string.Equals(tagName, "c", StringComparison.OrdinalIgnoreCase))
        {
            // Sometimes, the color tag is not closed correctly.
            // Then the <c> tag should be treated as TagClose.
            // e.g. <c=@reminder>Some text<c>
            if (iterator.Current?.Type != MarkupTokenType.TagValue)
            {
                return new ColoredTextNode("");
            }

            var node = new ColoredTextNode(iterator.Current.Value);
            iterator.Advance();
            while (iterator.Current?.Type != MarkupTokenType.TagClose && iterator.Current?.Type != MarkupTokenType.End)
            {
                var nextChild = ParseNode(iterator);
                if (nextChild is ColoredTextNode { Color: "" })
                {
                    // Treat the <c> tag as TagClose if the color tag is not closed correctly.
                    return node;
                }

                if (nextChild is not null)
                {
                    node.Children.Add(nextChild);
                }
            }

            if (iterator.Current?.Type == MarkupTokenType.TagClose)
            {
                iterator.Advance();
            }

            return node;
        }

        return null;
    }

    private static MarkupNode? ParseTextNode(MarkupTokenIterator iterator)
    {
        Debug.Assert(iterator.Current?.Type == MarkupTokenType.Text);
        var text = iterator.Current!.Value;
        iterator.Advance();
        return new TextNode(text);
    }
}
