using GuildWars2.Markup;

namespace GuildWars2.Tests.Features.Markup;

public class MarkupParserTest
{
    [Test]
    public void Ignores_invalid_tags()
    {
        string input = "5 <REDACTED> Dye kits";
        IEnumerable<MarkupToken> tokens = MarkupLexer.Tokenize(input);
        RootNode actual = MarkupParser.Parse(tokens);
        Assert.NotNull(actual);
        Assert.Collection(actual.Children, node =>
        {
            TextNode text = Assert.IsType<TextNode>(node);
            Assert.Equal("5 ", text.Text);
        }, node =>
        {
            TextNode text = Assert.IsType<TextNode>(node);
            Assert.Equal(" Dye kits", text.Text);
        });
    }

    [Test]
    public void Forgives_mismatched_tags()
    {
        string input = "<c=@reminder>This coat hides leg armor.<c>";
        IEnumerable<MarkupToken> tokens = MarkupLexer.Tokenize(input);
        RootNode actual = MarkupParser.Parse(tokens);
        Assert.NotNull(actual);
        MarkupNode firstChild = Assert.Single(actual.Children);
        ColoredTextNode coloredText = Assert.IsType<ColoredTextNode>(firstChild);
        Assert.Equal("@reminder", coloredText.Color);
        MarkupNode coloredTextChild = Assert.Single(coloredText.Children);
        TextNode text = Assert.IsType<TextNode>(coloredTextChild);
        Assert.Equal("This coat hides leg armor.", text.Text);
    }

    [Test]
    public void Keeps_trailing_newline()
    {
        string input = "<c=@flavor>A gift given in gratitude from the leaders of Tyria.</c>\n";
        IEnumerable<MarkupToken> tokens = MarkupLexer.Tokenize(input);
        RootNode actual = MarkupParser.Parse(tokens);
        Assert.NotNull(actual);
        Assert.Collection(actual.Children, node =>
        {
            ColoredTextNode coloredText = Assert.IsType<ColoredTextNode>(node);
            Assert.Equal("@flavor", coloredText.Color);
            MarkupNode coloredTextChild = Assert.Single(coloredText.Children);
            TextNode text = Assert.IsType<TextNode>(coloredTextChild);
            Assert.Equal("A gift given in gratitude from the leaders of Tyria.", text.Text);
        }, node =>
        {
            LineBreakNode lineBreak = Assert.IsType<LineBreakNode>(node);
        });
    }
}
