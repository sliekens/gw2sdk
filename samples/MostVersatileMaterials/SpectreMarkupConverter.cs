using System.Text;

using GuildWars2.Markup;

using Spectre.Console;

namespace MostVersatileMaterials;

internal sealed class SpectreMarkupConverter
{
    private static readonly Dictionary<string, string> ColorMap =
        new(StringComparer.OrdinalIgnoreCase)
        {
            [MarkupColorName.Flavor] = "#99dddd",
            [MarkupColorName.Reminder] = "#aaaaaa",
            [MarkupColorName.AbilityType] = "#ffee88",
            [MarkupColorName.Warning] = "#ff0000",
            [MarkupColorName.Task] = "#ffcc55"
        };

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

    private static string ConvertNode(MarkupNode node)
    {
        switch (node.Type)
        {
            case MarkupNodeType.Text:
                TextNode text = (TextNode)node;
                return Markup.Escape(text.Text);

            case MarkupNodeType.LineBreak:
                return Environment.NewLine;

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
