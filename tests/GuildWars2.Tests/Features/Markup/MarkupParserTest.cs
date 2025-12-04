using GuildWars2.Markup;

using Assert = TUnit.Assertions.Assert;

namespace GuildWars2.Tests.Features.Markup;

public class MarkupParserTest
{
    [Test]
    public async Task Ignores_invalid_tags()
    {
        string input = "5 <REDACTED> Dye kits";
        IEnumerable<MarkupToken> tokens = MarkupLexer.Tokenize(input);
        RootNode actual = MarkupParser.Parse(tokens);
        await Assert.That(actual)
            .IsNotNull()
            .And.Member(a => a.Children, a => a.Count().IsEqualTo(2));

        await Assert.That(actual.Children.ElementAt(0))
            .IsTypeOf<TextNode>()
            .And.Member(n => n.Text, t => t.IsEqualTo("5 "));

        await Assert.That(actual.Children.ElementAt(1))
            .IsTypeOf<TextNode>()
            .And.Member(n => n.Text, t => t.IsEqualTo(" Dye kits"));
    }

    [Test]
    public async Task Forgives_mismatched_tags()
    {
        string input = "<c=@reminder>This coat hides leg armor.<c>";
        IEnumerable<MarkupToken> tokens = MarkupLexer.Tokenize(input);
        RootNode actual = MarkupParser.Parse(tokens);
        await Assert.That(actual).IsNotNull();

        MarkupNode child = await Assert.That(actual.Children)
            .HasSingleItem();

        ColoredTextNode coloredText = await Assert.That(child)
            .IsTypeOf<ColoredTextNode>()
            .And.Member(n => n.Color, b => b.IsEqualTo("@reminder"))
            .And.IsNotNull();

        MarkupNode coloredTextChild = await Assert.That(coloredText.Children)
            .HasSingleItem();

        await Assert.That(coloredTextChild)
            .IsTypeOf<TextNode>()
            .And.Member(c => c.Text, d => d.IsEqualTo("This coat hides leg armor."));
    }

    [Test]
    public async Task Keeps_trailing_newline()
    {
        string input = "<c=@flavor>A gift given in gratitude from the leaders of Tyria.</c>\n";
        IEnumerable<MarkupToken> tokens = MarkupLexer.Tokenize(input);
        RootNode actual = MarkupParser.Parse(tokens);
        await Assert.That(actual)
            .IsNotNull()
            .And.Member(a => a.Children, a => a.Count().IsEqualTo(2));

        ColoredTextNode coloredText = await Assert.That(actual.Children.ElementAt(0))
            .IsTypeOf<ColoredTextNode>()
            .And.Member(n => n.Color, c => c.IsEqualTo("@flavor"))
            .And.Member(n => n.Children, c => c.HasSingleItem())
            .And.IsNotNull();

        MarkupNode coloredTextChild = await Assert.That(coloredText.Children).HasSingleItem();
        await Assert.That(coloredTextChild)
            .IsTypeOf<TextNode>()
            .And.Member(text => text.Text, text => text.IsEqualTo("A gift given in gratitude from the leaders of Tyria."));

        await Assert.That(actual.Children.ElementAt(1)).IsTypeOf<LineBreakNode>();
    }
}
