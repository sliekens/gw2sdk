﻿using GuildWars2.Markup;

namespace GuildWars2.Tests.Features.Markup;

public class MarkupParserTest
{
    [Fact]
    public void Ignores_invalid_tags()
    {
        var input = "5 <REDACTED> Dye kits";
        var lexer = new MarkupLexer();
        var parser = new MarkupParser();
        var tokens = lexer.Tokenize(input);
        var actual = parser.Parse(tokens);

        Assert.NotNull(actual);
        Assert.Collection(
            actual.Children,
            node =>
            {
                var text = Assert.IsType<TextNode>(node);
                Assert.Equal("5 ", text.Text);
            },
            node =>
            {
                var text = Assert.IsType<TextNode>(node);
                Assert.Equal(" Dye kits", text.Text);
            }
        );
    }

    [Fact]
    public void Forgives_mismatched_tags()
    {
        var input = "<c=@reminder>This coat hides leg armor.<c>";
        var lexer = new MarkupLexer();
        var parser = new MarkupParser();
        var tokens = lexer.Tokenize(input);
        var actual = parser.Parse(tokens);

        Assert.NotNull(actual);
        Assert.Collection(
            actual.Children,
            node =>
            {
                var coloredText = Assert.IsType<ColoredTextNode>(node);
                Assert.Equal("@reminder", coloredText.Color);
                Assert.Collection(
                    coloredText.Children,
                    node =>
                    {
                        var text = Assert.IsType<TextNode>(node);
                        Assert.Equal("This coat hides leg armor.", text.Text);
                    }
                );
            }
        );
    }

    [Fact]
    public void Keeps_trailing_newline()
    {
        var input = "<c=@flavor>A gift given in gratitude from the leaders of Tyria.</c>\n";
        var lexer = new MarkupLexer();
        var parser = new MarkupParser();
        var tokens = lexer.Tokenize(input);
        var actual = parser.Parse(tokens);

        Assert.NotNull(actual);
        Assert.Collection(
            actual.Children,
            node =>
            {
                var coloredText = Assert.IsType<ColoredTextNode>(node);
                Assert.Equal("@flavor", coloredText.Color);
                Assert.Collection(
                    coloredText.Children,
                    node =>
                    {
                        var text = Assert.IsType<TextNode>(node);
                        Assert.Equal(
                            "A gift given in gratitude from the leaders of Tyria.",
                            text.Text
                        );
                    }
                );
            },
            node =>
            {
                var lineBreak = Assert.IsType<LineBreakNode>(node);
            }
        );
    }
}
