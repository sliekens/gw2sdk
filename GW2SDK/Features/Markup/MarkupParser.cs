using System.Diagnostics;

namespace GuildWars2.Markup;

/// <summary>
/// Represents a parser that converts a sequence of tokens into a hierarchical node structure.
/// </summary>
[PublicAPI]
public sealed class MarkupParser(IEnumerable<MarkupToken> input)
{
    private readonly List<MarkupToken> tokens = input.ToList();

    private int position;

    private void Advance() => position++;
    private MarkupToken? Current => position < tokens.Count ? tokens[position] : null;

    /// <summary>
    /// Parses a sequence of tokens into a hierarchical node structure.
    /// </summary>
    /// <returns>The root node of the parsed structure.</returns>
    public RootNode Parse()
    {
        var root = new RootNode();
        while (Current?.Type != MarkupTokenType.End)
        {
            var node = ParseNode();
            if (node is not null)
            {
                root.Children.Add(node);
            }
        }

        return root;
    }

    private MarkupNode? ParseNode()
    {
        switch (Current?.Type)
        {
            case MarkupTokenType.Text:
                return ParseTextNode();
            case MarkupTokenType.TagStart:
                return ParseTagNode();
            case MarkupTokenType.TagVoid:
                return ParseVoidNode();
            default:
                Advance();
                return null;
        };
    }

    private MarkupNode? ParseVoidNode()
    {
        Debug.Assert(Current?.Type == MarkupTokenType.TagVoid);
        var tagName = Current!.Value;
        Advance();

        if (string.Equals(tagName, "br", StringComparison.OrdinalIgnoreCase))
        {
            return new LineBreakNode();
        }

        return null;
    }

    private MarkupNode? ParseTagNode()
    {
        Debug.Assert(Current?.Type == MarkupTokenType.TagStart);
        var tagName = Current!.Value;
        Advance();

        if (string.Equals(tagName, "c", StringComparison.OrdinalIgnoreCase))
        {
            var color = "";
            if (Current?.Type == MarkupTokenType.TagValue)
            {
                color = Current.Value;
                Advance();
            }

            var coloredText = new ColoredTextNode(color);
            while (Current?.Type != MarkupTokenType.TagClose && Current?.Type != MarkupTokenType.End)
            {
                var node = ParseNode();
                if (node is not null)
                {
                    coloredText.Children.Add(node);
                }
            }

            if (Current?.Type == MarkupTokenType.TagClose)
            {
                Advance();
            }

            return coloredText;
        }

        return null;
    }

    private MarkupNode? ParseTextNode()
    {
        Debug.Assert(Current?.Type == MarkupTokenType.Text);
        var text = Current!.Value;
        Advance();
        return new TextNode(text);
    }
}
