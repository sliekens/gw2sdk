using GuildWars2.Markup;

using Spectre.Console;

using SpectreMarkup;

string input = """
    Double-click to apply to an unused infusion slot. Adds a festive glow.
    <c=@Warning>Warning!</c>
    <c=@Flavor>Captain's Council recommends avoiding direct contact with this substance.</c>
    """;

IEnumerable<MarkupToken> tokens = MarkupLexer.Tokenize(input);
RootNode syntax = MarkupParser.Parse(tokens);
string markup = SpectreMarkupConverter.Convert(syntax);

AnsiConsole.MarkupLine(markup);
