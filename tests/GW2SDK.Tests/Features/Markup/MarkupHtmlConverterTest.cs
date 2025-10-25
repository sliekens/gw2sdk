using GuildWars2.Markup;

namespace GuildWars2.Tests.Features.Markup;

public class MarkupHtmlConverterTest
{
    [Test]
    public void Converts_color_tags_to_span_elements()
    {
        string input = "This is <c=@flavor>flavor</c> text and this is <c=#ff0000>inline styled</c> text.";
        string actual = MarkupConverter.ToHtml(input);
        Assert.Equal("This is <span style=\"color: #99eedd\">flavor</span> text and this is <span style=\"color: #ff0000\">inline styled</span> text.", actual);
    }

    [Test]
    public void Converts_br()
    {
        string input = "Line 1.<br>Line 2.<br/>Line 3.<br />Line 4.";
        string actual = MarkupConverter.ToHtml(input);
        Assert.Equal("Line 1.<br>Line 2.<br>Line 3.<br>Line 4.", actual);
    }

    [Test]
    public void Converts_line_feeds()
    {
        string input = "Line 1.\nLine 2.\nLine 3.\nLine 4.";
        string actual = MarkupConverter.ToHtml(input);
        Assert.Equal("Line 1.<br>Line 2.<br>Line 3.<br>Line 4.", actual);
    }
}
