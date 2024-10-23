using System.Text;
using GuildWars2.Markup;
using Spectre.Console;

namespace MostVersatileMaterials;

public class SpectreMarkupConverter
{
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
                return Environment.NewLine;
            case ColoredTextNode coloredText:
                var content = string.Concat(coloredText.Children.Select(ConvertNode)).EscapeMarkup();
                if (coloredText.Color.StartsWith("#"))
                {
                    return $"[${coloredText.Color}]{content}[/]";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Flavor, StringComparison.OrdinalIgnoreCase))
                {
                    return $"[#9BE8E4]{content}[/]";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Reminder, StringComparison.OrdinalIgnoreCase))
                {
                    return $"[#B0B0B0]{content}[/]";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.AbilityType, StringComparison.OrdinalIgnoreCase))
                {
                    return $"[#FFEC8C]{content}[/]";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Warning, StringComparison.OrdinalIgnoreCase))
                {
                    return $"[#ED0002]{content}[/]";
                }
                else if (string.Equals(coloredText.Color, MarkupColorName.Task, StringComparison.OrdinalIgnoreCase))
                {
                    return $"[#FFC957]{content}[/]";
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
