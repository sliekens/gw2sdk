using GuildWars2.Markup;

namespace GuildWars2.Tests.Features.Markup;

public static class MarkupSyntaxValidator
{
    public static void Validate(string input)
    {
        IEnumerable<MarkupToken> tokens = MarkupLexer.Tokenize(input);
        RootNode syntax = MarkupParser.Parse(tokens);
        Validate(syntax);
    }

    private static void Validate(RootNode root)
    {
        foreach (MarkupNode? node in root.Children)
        {
            ValidateNode(node);
        }
    }

    private static void ValidateNode(MarkupNode node)
    {
        switch (node)
        {
            case TextNode text:
                ValidateTextNode(text);
                break;
            case ColoredTextNode coloredText:
                ValidateColoredTextNode(coloredText);
                break;
            case LineBreakNode:
                break;
            default:
                Assert.Fail($"Unexpected node type: {node.GetType().Name}");
                break;
        }
    }

    private static void ValidateTextNode(TextNode text)
    {
        Assert.Matches("[a-zA-Z0-9 .]+", text.Text);
    }

    private static void ValidateColoredTextNode(ColoredTextNode coloredText)
    {
        Assert.NotEmpty(coloredText.Color);
#if NET
        if (coloredText.Color.StartsWith('@'))
#else
        if (coloredText.Color.StartsWith("@", StringComparison.Ordinal))
#endif
        {
            if (coloredText.Color != "@warn")
            {
                Assert.True(
                    MarkupColorName.IsDefined(coloredText.Color),
                    $"Unexpected color name: {coloredText.Color}."
                );
            }
        }
#if NET
        else if (coloredText.Color.StartsWith('#'))
#else
        else if (coloredText.Color.StartsWith("#", StringComparison.Ordinal))
#endif
        {
            if (coloredText.Color is not ("#Flavor" or "#Warning"))
            {
                Assert.Matches("#[0-9a-fA-F]{6}", coloredText.Color);
            }
        }
        else
        {
            // Sometimes they forget the '@' symbol and it doesn't actually color the text in-game
            if (!MarkupColorName.IsDefined($"@{coloredText.Color}"))
            {
                throw new FormatException($"Invalid color format: {coloredText.Color}.");
            }
        }

        foreach (MarkupNode? child in coloredText.Children)
        {
            ValidateNode(child);
        }
    }
}
