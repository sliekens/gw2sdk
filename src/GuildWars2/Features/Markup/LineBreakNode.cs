namespace GuildWars2.Markup;

/// <summary>Represents a line break node in the markup structure.</summary>
public sealed class LineBreakNode : MarkupNode
{
    /// <inheritdoc />
    public override MarkupNodeType Type => MarkupNodeType.LineBreak;
}
