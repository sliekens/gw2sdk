using GuildWars2.Markup;

namespace GuildWars2.Tests.Features.Markup;

public class MarkupHtmlConverterTest
{
    [Fact]
    public void Converts_color_tags_to_span_elements()
    {
        var input = "This is <c=@flavor>flavor</c> text and this is <c=#FF0000>inline styled</c> text.";
        var lexer = new MarkupLexer();
        var parser = new MarkupParser();
        var converter = new MarkupHtmlConverter();
        var tokens = lexer.Tokenize(input);
        var syntax = parser.Parse(tokens);
        var actual = converter.Convert(syntax);

        Assert.Equal(
            "This is <span style=\"color: #9BE8E4\">flavor</span> text and this is <span style=\"color: #FF0000\">inline styled</span> text.",
            actual
        );
    }

    [Fact]
    public void Converts_br()
    {
        var input = "Line 1.<br>Line 2.<br/>Line 3.<br />Line 4.";
        var lexer = new MarkupLexer();
        var parser = new MarkupParser();
        var converter = new MarkupHtmlConverter();
        var tokens = lexer.Tokenize(input);
        var syntax = parser.Parse(tokens);
        var actual = converter.Convert(syntax);

        Assert.Equal(
            "Line 1.<br>Line 2.<br>Line 3.<br>Line 4.",
            actual
        );
    }

    [Fact]
    public void Converts_line_feeds()
    {
        var input = "Line 1.\nLine 2.\nLine 3.\nLine 4.";
        var lexer = new MarkupLexer();
        var parser = new MarkupParser();
        var converter = new MarkupHtmlConverter();
        var tokens = lexer.Tokenize(input);
        var syntax = parser.Parse(tokens);
        var actual = converter.Convert(syntax);

        Assert.Equal(
            "Line 1.<br>Line 2.<br>Line 3.<br>Line 4.",
            actual
        );
    }
}
