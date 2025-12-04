using GuildWars2.Markup;

namespace GuildWars2.Tests.Features.Markup;

public class MarkupHtmlConverterTest
{
    [Test]
    public async Task Converts_color_tags_to_span_elements()
    {
        string input = "This is <c=@flavor>flavor</c> text and this is <c=#ff0000>inline styled</c> text.";
        string actual = MarkupConverter.ToHtml(input);
        await Assert.That(actual).IsEqualTo("This is <span style=\"color: #99eedd\">flavor</span> text and this is <span style=\"color: #ff0000\">inline styled</span> text.");
    }

    [Test]
    public async Task Converts_br()
    {
        string input = "Line 1.<br>Line 2.<br/>Line 3.<br />Line 4.";
        string actual = MarkupConverter.ToHtml(input);
        await Assert.That(actual).IsEqualTo("Line 1.<br>Line 2.<br>Line 3.<br>Line 4.");
    }

    [Test]
    public async Task Converts_line_feeds()
    {
        string input = "Line 1.\nLine 2.\nLine 3.\nLine 4.";
        string actual = MarkupConverter.ToHtml(input);
        await Assert.That(actual).IsEqualTo("Line 1.<br>Line 2.<br>Line 3.<br>Line 4.");
    }
}
