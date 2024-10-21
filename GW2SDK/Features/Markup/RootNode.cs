namespace GuildWars2.Markup;

/// <summary>
/// Represents a root node in the markup structure.
/// </summary>
[PublicAPI]
public sealed class RootNode : MarkupNode
{
    /// <inheritdoc />
    public override MarkupNodeType Type => MarkupNodeType.Root;

    /// <summary>
    /// Gets the list of child nodes.
    /// </summary>
    public List<MarkupNode> Children { get; } = [];
}
