namespace GuildWars2.Markup;

/// <summary>
/// Represents a text node in the markup structure.
/// /// </summary>
[PublicAPI]
public sealed class TextNode(string text) : MarkupNode
{
    /// <inheritdoc />
    public override MarkupNodeType Type => MarkupNodeType.Text;

    /// <summary>
    /// Gets the text content of the node.
    /// </summary>
    public string Text => text;
}
