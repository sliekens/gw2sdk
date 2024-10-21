using GuildWars2.Markup;

namespace GuildWars2.Tests.Features.Markup;

public class MarkupParserTest
{
    [Fact]
    public void Ignores_invalid_tags()
    {
        var input = "5 <REDACTED> Dye kits";
        var lexer = new MarkupLexer();
        var parser = new MarkupParser(lexer.Tokenize(input));

        var actual = parser.Parse();

        Assert.NotNull(actual);
        Assert.Collection(actual.Children,
            node =>
            {
                var text = Assert.IsType<TextNode>(node);
                Assert.Equal("5 ", text.Text);
            },
            node =>
            {
                var text = Assert.IsType<TextNode>(node);
                Assert.Equal(" Dye kits", text.Text);
            });
    }
}
