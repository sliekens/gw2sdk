using System.Text;
using GuildWars2.Markup;

namespace GuildWars2.Features.Markup;

/// <summary>
/// Provides functionality to convert a <see cref="RootNode"/> to a string representation.
/// </summary>
[PublicAPI]
public sealed class MarkupTextConverter
{
    /// <summary>
    /// Converts a <see cref="RootNode"/> to a string representation.
    /// </summary>
    /// <param name="root">The root node containing nodes to be converted.</param>
    /// <returns>A string representation of the nodes within the root node.</returns>
    public string Convert(RootNode root)
    {
        var builder = new StringBuilder();
        foreach (var node in root.Children)
        {
            switch (node)
            {
                case TextNode text:
                    builder.Append(text.Text);
                    break;
                case LineBreakNode _:
                    builder.AppendLine();
                    break;
            }
        }

        return builder.ToString();
    }

    private string ConvertNode(MarkupNode node)
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
