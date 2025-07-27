namespace GuildWars2.Markup;

/// <summary>Provides functionality to convert markup strings to other formats.</summary>
[PublicAPI]
public static class MarkupConverter
{
    private static readonly MarkupLexer Lexer = new();

    private static readonly MarkupParser Parser = new();

    private static readonly MarkupTextConverter TextConverter = new();

    private static readonly MarkupHtmlConverter HtmlConverter = new();

    /// <summary>Converts a markup string to a string with all markup formatting removed.</summary>
    /// <param name="markup">The markup string to convert.</param>
    /// <returns>The text with all markup formatting removed.</returns>
    public static string ToPlainText(string markup)
    {
        if (string.IsNullOrWhiteSpace(markup))
        {
            return markup;
        }

        var tokens = Lexer.Tokenize(markup);
        var rootNode = Parser.Parse(tokens);
        return TextConverter.Convert(rootNode);
    }

    /// <summary>Converts a markup string to a string with HTML formatting using the
    /// <see cref="MarkupColorName.DefaultColorMap" />.</summary>
    /// <param name="markup">The markup string to convert.</param>
    /// <returns>The HTML string.</returns>
    public static string ToHtml(string markup)
    {
        if (string.IsNullOrWhiteSpace(markup))
        {
            return markup;
        }

        var tokens = Lexer.Tokenize(markup);
        var rootNode = Parser.Parse(tokens);
        return HtmlConverter.Convert(rootNode);
    }

    /// <summary>Converts a markup string to a string with HTML formatting using a custom color map.</summary>
    /// <param name="markup">The markup string to convert.</param>
    /// <param name="colorMap">A dictionary mapping color names to their corresponding HTML color codes.</param>
    /// <returns>The HTML string.</returns>
    public static string ToHtml(string markup, IReadOnlyDictionary<string, string> colorMap)
    {
        if (string.IsNullOrWhiteSpace(markup))
        {
            return markup;
        }

        var tokens = Lexer.Tokenize(markup);
        var rootNode = Parser.Parse(tokens);
        return HtmlConverter.Convert(rootNode, colorMap);
    }
}
