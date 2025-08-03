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

        var builder = new StringBuilder();
        foreach (var node in root.Children)
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
                var text = (TextNode)node;
                return Markup.Escape(text.Text);

            case MarkupNodeType.LineBreak:
                return Environment.NewLine;

            case MarkupNodeType.ColoredText:
                var coloredText = (ColoredTextNode)node;
                var builder = new StringBuilder();
                foreach (var child in coloredText.Children)
                {
                    builder.Append(ConvertNode(child));
                }

                var content = builder.ToString();
                if (coloredText.Color.StartsWith('#'))
                {
                    var colorCode = coloredText.Color;
                    return $"[{colorCode}]{content}[/]";
                }
                else if (ColorMap.TryGetValue(coloredText.Color, out var colorCode))
                {
                    return $"[{colorCode}]{content}[/]";
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
