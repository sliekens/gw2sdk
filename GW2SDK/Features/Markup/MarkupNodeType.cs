using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Markup;

/// <summary>
/// Represents the types of nodes that can be encountered in the markup lexer.
/// </summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(MarkupNodeTypeJsonConverter))]
public enum MarkupNodeType
{
    /// <summary>
    /// No node type.
    /// </summary>
    None,

    /// <summary>
    /// A root node.
    /// </summary>
    Root,

    /// <summary>
    /// A text node, used to represent plain text in the markup.
    /// </summary>
    Text,

    /// <summary>
    /// A colored text node, used to represent text with a specific color in the markup.
    /// </summary>
    ColoredText,

    /// <summary>
    /// A line break node, used to represent a line break in the markup.
    /// </summary>
    LineBreak
}
