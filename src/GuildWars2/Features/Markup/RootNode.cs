namespace GuildWars2.Markup;

/// <summary>Represents a root node in the markup structure.</summary>
/// <param name="children">The child nodes.</param>
public sealed class RootNode(IImmutableList<MarkupNode> children) : MarkupNode
{
    /// <inheritdoc />
    public override MarkupNodeType Type => MarkupNodeType.Root;

    /// <summary>Gets the child nodes.</summary>
    public IImmutableList<MarkupNode> Children { get; } = children;
}
