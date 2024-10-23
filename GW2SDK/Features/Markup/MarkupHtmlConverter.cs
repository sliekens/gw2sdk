using System.Text;

namespace GuildWars2.Markup;

/// <summary>
/// Provides functionality to convert a <see cref="RootNode"/> to its HTML representation.
/// </summary>
[PublicAPI]
public sealed class MarkupHtmlConverter
{
    /// <summary>
    /// Converts a <see cref="RootNode"/> to its HTML representation.
    /// </summary>
    /// <param name="root">The root node containing nodes to be converted.</param>
    /// <returns>A string representation of the nodes within the root node.</returns>
    public string Convert(RootNode root)
    {
        var builder = new StringBuilder();
        foreach (var node in root.Children)
        {
            builder.Append(ConvertNode(node));
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
                return "<br>";
            case ColoredTextNode coloredText:
                var content = string.Concat(coloredText.Children.Select(ConvertNode));
                if (coloredText.Color.StartsWith("#"))
                {
                    return $"<span style=\"color: {coloredText.Color}\">{content}</span>";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Flavor, StringComparison.OrdinalIgnoreCase))
                {
                    return $"<span style=\"color: #9BE8E4\">{content}</span>";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Reminder, StringComparison.OrdinalIgnoreCase))
                {
                    return $"<span style=\"color: #B0B0B0\">{content}</span>";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.AbilityType, StringComparison.OrdinalIgnoreCase))
                {
                    return $"<span style=\"color: #FFEC8C\">{content}</span>";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Warning, StringComparison.OrdinalIgnoreCase))
                {
                    return $"<span style=\"color: #ED0002\">{content}</span>";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Task, StringComparison.OrdinalIgnoreCase))
                {
                    return $"<span style=\"color: #FFC957\">{content}</span>";
                }
                else
                {
                    return content;
                }
            default:
                return "";
        }
    }
}
