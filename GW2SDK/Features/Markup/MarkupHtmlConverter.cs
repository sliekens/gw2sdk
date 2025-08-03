using System.Text;

namespace GuildWars2.Markup;

/// <summary>Provides functionality to convert a <see cref="RootNode" /> to its HTML representation.</summary>
[PublicAPI]
public sealed class MarkupHtmlConverter
{
    /// <summary>Converts a <see cref="RootNode" /> to its HTML representation using the
    /// <see cref="MarkupColorName.DefaultColorMap" />.</summary>
    /// <param name="root">The root node of the markup syntax tree to convert.</param>
    /// <returns>A string containing the HTML representation of the markup syntax tree.</returns>
    public static string Convert(RootNode root)
    {
        return Convert(root, MarkupColorName.DefaultColorMap);
    }

    /// <summary>Converts a <see cref="RootNode" /> and its children to an HTML string representation using a custom color map.</summary>
    /// <param name="root">The root node of the markup syntax tree to convert.</param>
    /// <param name="colorMap">A dictionary mapping color names to their corresponding HTML color codes.</param>
    /// <returns>A string containing the HTML representation of the markup syntax tree.</returns>
    public static string Convert(RootNode root, IReadOnlyDictionary<string, string>? colorMap)
    {
        ThrowHelper.ThrowIfNull(root);

        if (colorMap is null)
        {
            colorMap = MarkupColorName.DefaultColorMap;
        }
        else if (colorMap != MarkupColorName.DefaultColorMap)
        {
            // Ensure the key comparison is case-insensitive
            colorMap = colorMap.ToDictionary(
                pair => pair.Key,
                pair => pair.Value,
                StringComparer.OrdinalIgnoreCase
            );
        }

        StringBuilder builder = new();
        foreach (var node in root.Children)
        {
            builder.Append(ConvertNode(node, colorMap));
        }

        return builder.ToString();
    }

    private static string ConvertNode(MarkupNode node, IReadOnlyDictionary<string, string> colorMap)
    {
        switch (node)
        {
            case TextNode text:
                return text.Text;
            case LineBreakNode:
                return "<br>";
            case ColoredTextNode coloredText:
                StringBuilder builder = new();
                foreach (var child in coloredText.Children)
                {
                    builder.Append(ConvertNode(child, colorMap));
                }

                var content = builder.ToString();
#if NET
                if (coloredText.Color.StartsWith('#'))
#else
                if (coloredText.Color.StartsWith("#", StringComparison.Ordinal))
#endif
                {
                    return $"<span style=\"color: {coloredText.Color}\">{content}</span>";
                }

                if (colorMap.TryGetValue(coloredText.Color, out var color))
                {
                    return $"<span style=\"color: {color}\">{content}</span>";
                }

                return content;
            default:
                return "";
        }
    }
}
