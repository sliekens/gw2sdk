namespace GuildWars2.Markup;

/// <summary>Represents a colored text node in the markup structure.</summary>
/// <param name="color">The color of the text node.</param>
/// <param name="children">The child nodes.</param>
public sealed class ColoredTextNode(string color, IImmutableList<MarkupNode> children) : MarkupNode
{
    /// <inheritdoc />
    public override MarkupNodeType Type => MarkupNodeType.ColoredText;

    /// <summary>The color of the text.</summary>
    public string Color => color;

    /// <summary>Gets the child nodes.</summary>
    public IImmutableList<MarkupNode> Children { get; } = children;
}
