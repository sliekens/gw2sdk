using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace GuildWars2.Markup;

/// <summary>Represents a parser that converts a sequence of tokens into a hierarchical node structure.</summary>
/// <remarks>Initializes a new instance of the <see cref="MarkupParser"/> class.</remarks>
public static class MarkupParser
{
    /// <summary>Parses a sequence of tokens into a hierarchical node structure.</summary>
    /// <param name="input">The sequence of markup tokens to parse.</param>
    /// <returns>The root node of the parsed structure.</returns>
    public static RootNode Parse(IEnumerable<MarkupToken> input)
    {
        MarkupTokenIterator iterator = new(input);
        ImmutableArray<MarkupNode>.Builder children = ImmutableArray.CreateBuilder<MarkupNode>();
        while (iterator.Current is { Type: not MarkupTokenType.End })
        {
            MarkupNode? node = ParseNode(iterator);
            if (node is not null)
            {
                children.Add(node);
            }
        }

        return new RootNode(children.ToImmutable());
    }

    private static MarkupNode? ParseNode(MarkupTokenIterator iterator)
    {
        switch (iterator.Current?.Type)
        {
            case MarkupTokenType.Text:
                string text = iterator.Current.Value;
                iterator.Advance();
                return new TextNode(text);
            case MarkupTokenType.LineBreak:
                iterator.Advance();
                return new LineBreakNode();
            case MarkupTokenType.TagStart:
                return ParseTagNode(iterator);
            case MarkupTokenType.TagVoid:
                return ParseVoidNode(iterator);
            case MarkupTokenType.None:
            case MarkupTokenType.TagValue:
            case MarkupTokenType.TagClose:
            case MarkupTokenType.End:
            case null:
            default:
                iterator.Advance();
                return null;
        }
    }

    [SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance", Justification = "More node types may be added in the future.")]
    private static MarkupNode? ParseVoidNode(MarkupTokenIterator iterator)
    {
        Debug.Assert(iterator.Current?.Type == MarkupTokenType.TagVoid);
        string tagName = iterator.Current!.Value;
        iterator.Advance();

        if (string.Equals(tagName, "br", StringComparison.OrdinalIgnoreCase))
        {
            return new LineBreakNode();
        }

        return null;
    }

    [SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance", Justification = "More node types may be added in the future.")]
    private static MarkupNode? ParseTagNode(MarkupTokenIterator iterator)
    {
        Debug.Assert(iterator.Current?.Type == MarkupTokenType.TagStart);
        string tagName = iterator.Current!.Value;
        iterator.Advance();

        if (string.Equals(tagName, "c", StringComparison.OrdinalIgnoreCase))
        {
            // Sometimes, the color tag is not closed correctly.
            // Then the <c> tag should be treated as TagClose.
            // e.g. <c=@reminder>Some text<c>
            if (iterator.Current?.Type != MarkupTokenType.TagValue)
            {
                return new ColoredTextNode("", ImmutableArray<MarkupNode>.Empty);
            }

            string color = iterator.Current.Value;
            iterator.Advance();
            ImmutableArray<MarkupNode>.Builder children = ImmutableArray.CreateBuilder<MarkupNode>();
            while (iterator.Current is { Type: not MarkupTokenType.TagClose and not MarkupTokenType.End })
            {
                MarkupNode? nextChild = ParseNode(iterator);
                if (nextChild is ColoredTextNode { Color: "" })
                {
                    // Treat the <c> tag as TagClose if the color tag is not closed correctly.
                    return new ColoredTextNode(color, children.ToImmutable());
                }

                if (nextChild is not null)
                {
                    children.Add(nextChild);
                }
            }

            if (iterator.Current?.Type == MarkupTokenType.TagClose)
            {
                iterator.Advance();
            }

            return new ColoredTextNode(color, children.ToImmutable());
        }

        return null;
    }
}
