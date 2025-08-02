using System.Text;

namespace GuildWars2.Markup;

/// <summary>Provides functionality to convert a <see cref="RootNode" /> to a string representation.</summary>
[PublicAPI]
public sealed class MarkupTextConverter
{
    /// <summary>Converts a <see cref="RootNode" /> to a string representation.</summary>
    /// <param name="root">The root node containing nodes to be converted.</param>
    /// <returns>A string representation of the nodes within the root node.</returns>
    public static string Convert(RootNode root)
    {
        ThrowHelper.ThrowIfNull(root);

        StringBuilder builder = new();
        foreach (var node in root.Children)
        {
            builder.Append(ConvertNode(node));
        }

        return builder.ToString();
    }

    private static string ConvertNode(MarkupNode node)
    {
        switch (node)
        {
            case TextNode text:
                return text.Text;
            case LineBreakNode:
                return Environment.NewLine;
            case ColoredTextNode coloredText:
                return string.Concat(coloredText.Children.Select(ConvertNode));
            default:
                return "";
        }
    }
}
