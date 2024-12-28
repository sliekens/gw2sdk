namespace GuildWars2.Markup;

/// <summary>Represents a colored text node in the markup structure.</summary>
[PublicAPI]
public sealed class ColoredTextNode(string color) : MarkupNode
{
    /// <inheritdoc />
    public override MarkupNodeType Type => MarkupNodeType.ColoredText;

    /// <summary>The color of the text.</summary>
    public string Color => color;

    /// <summary>Gets the list of child nodes.</summary>
    public List<MarkupNode> Children { get; } = [];
}
