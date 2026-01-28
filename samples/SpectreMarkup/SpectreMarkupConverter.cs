using System.Text;

using GuildWars2.Markup;

using Spectre.Console;

namespace SpectreMarkup;

internal static class SpectreMarkupConverter
{
    // A map of color names to hexadecimal RGB values.
    // Note that the color names are case-insensitive.
    private static readonly Dictionary<string, string> ColorMap =
        new(comparer: StringComparer.OrdinalIgnoreCase)
        {
            [MarkupColorName.Flavor] = "#99dddd",
            [MarkupColorName.Reminder] = "#aaaaaa",
            [MarkupColorName.AbilityType] = "#ffee88",
            [MarkupColorName.Warning] = "#ff0000",
            [MarkupColorName.Task] = "#ffcc55",
        };

    // This is the entry point for the conversion.
    // Input: The ROOT node of the syntax tree.
    // Output: the formatted text that can be understood by your UI framework.
    //   In this Spectre.Console example, the return type is a string,
    //   but it could also be an object like System.Windows.Media.FormattedText.
    public static string Convert(RootNode root)
    {
        ArgumentNullException.ThrowIfNull(root);

        StringBuilder builder = new();
        foreach (MarkupNode node in root.Children)
        {
            builder.Append(ConvertNode(node));
        }

        return builder.ToString();
    }

    // This method converts a single node in the syntax tree to the desired format.
    // Input: the CURRENT node encountered while traversing the syntax tree.
    // Output: the formatted text that can be understood by your UI framework.
    //   In this Spectre.Console example, the return type is a string,
    //   but it could also be an object like System.Windows.Media.FormattedText.
    private static string ConvertNode(MarkupNode node)
    {
        // MarkupNode is the base type for all nodes in the syntax tree.
        // Use a switch statement to convert each type of node to the desired format.
        // You could also use pattern matching with C# 9.
        switch (node.Type)
        {
            // TextNode is just a plain text node, no formatting.
            case MarkupNodeType.Text:
                TextNode text = (TextNode)node;
                return Markup.Escape(text.Text);

            // LineBreakNode represents a line break, covers both \n and <br>
            case MarkupNodeType.LineBreak:
                return Environment.NewLine;

            // ColoredTextNode represents text with a color like <c=#ff000>text</c>
            // or <c=@warning>text</c>
            case MarkupNodeType.ColoredText:
                ColoredTextNode coloredText = (ColoredTextNode)node;
                StringBuilder builder = new();
                foreach (MarkupNode child in coloredText.Children)
                {
                    builder.Append(ConvertNode(child));
                }

                string content = builder.ToString();
                if (coloredText.Color.StartsWith('#'))
                {
                    string colorCode = coloredText.Color;
                    return $"[{colorCode}]{content}[/]";
                }
                else if (ColorMap.TryGetValue(coloredText.Color, out string? colorCode))
                {
                    return $"[{colorCode}]{content}[/]";
                }
                else
                {
                    return content;
                }

            case MarkupNodeType.None:
            case MarkupNodeType.Root:
            default:
                return "";
        }
    }
}
