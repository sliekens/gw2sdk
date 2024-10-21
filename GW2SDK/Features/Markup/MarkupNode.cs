namespace GuildWars2.Markup;

/// <summary>
/// Represents a node in the markup structure.
/// </summary>
[PublicAPI]
public abstract class MarkupNode
{
    /// <summary>
    /// Gets the type of the node.
    /// </summary>
    /// <value>
    /// The type of the node, represented by the <see cref="MarkupNodeType"/> enumeration.
    /// </value>
    public abstract MarkupNodeType Type { get; }

    /// <inheritdoc/>
    override public string ToString() => Type.ToString();
}
