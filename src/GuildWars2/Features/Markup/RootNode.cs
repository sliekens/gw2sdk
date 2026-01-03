namespace GuildWars2.Markup;

/// <summary>Represents a root node in the markup structure.</summary>
public sealed class RootNode : MarkupNode
{
    /// <inheritdoc />
    public override MarkupNodeType Type => MarkupNodeType.Root;

#pragma warning disable CA1002 // Do not expose generic lists
    // TODO: reconsider collection type
    /// <summary>Gets the list of child nodes.</summary>
    public List<MarkupNode> Children { get; } = [];
#pragma warning restore CA1002 // Do not expose generic lists
}
