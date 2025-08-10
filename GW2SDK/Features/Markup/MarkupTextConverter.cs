using System.Text;

namespace GuildWars2.Markup;

/// <summary>Provides functionality to convert a <see cref="RootNode" /> to a string representation.</summary>
/// <remarks>Initializes a new instance of the <see cref="MarkupTextConverter"/> class.</remarks>
[method: Obsolete("MarkupTextConverter methods are now static. Use static methods instead.")]
public sealed class MarkupTextConverter()
{
    /// <summary>Converts a <see cref="RootNode" /> to a string representation.</summary>
    /// <param name="root">The root node containing nodes to be converted.</param>
    /// <returns>A string representation of the nodes within the root node.</returns>
    public static string Convert(RootNode root)
    {
        ThrowHelper.ThrowIfNull(root);

        StringBuilder builder = new();
        foreach (MarkupNode node in root.Children)
        {
            builder.Append(ConvertNode(node));
        }

        return builder.ToString();
    }

    private static string ConvertNode(MarkupNode node)
    {
        return node switch
        {
            TextNode text => text.Text,
            LineBreakNode => Environment.NewLine,
            ColoredTextNode coloredText => string.Concat(coloredText.Children.Select(ConvertNode)),
            _ => "",
        };
    }
}
