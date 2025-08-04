namespace GuildWars2.Markup;

/// <summary>Represents a node in the markup structure.</summary>
public abstract class MarkupNode
{
    /// <summary>Gets the type of the node.</summary>
    /// <value>The type of the node, represented by the <see cref="MarkupNodeType" /> enumeration.</value>
    public abstract MarkupNodeType Type { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Type.ToString();
    }
}
