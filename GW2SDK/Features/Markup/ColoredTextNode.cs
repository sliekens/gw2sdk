namespace GuildWars2.Markup;

/// <summary>Represents a colored text node in the markup structure.</summary>
/// <param name="color">The color of the text node.</param>
[PublicAPI]
public sealed class ColoredTextNode(string color) : MarkupNode
{
    /// <inheritdoc />
    public override MarkupNodeType Type => MarkupNodeType.ColoredText;

    /// <summary>The color of the text.</summary>
    public string Color => color;

#pragma warning disable CA1002 // Do not expose generic lists
    // TODO: reconsider collection type
    /// <summary>Gets the list of child nodes.</summary>
    public List<MarkupNode> Children { get; } = [];
#pragma warning restore CA1002 // Do not expose generic lists
}
