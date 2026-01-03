namespace GuildWars2.Markup;

internal enum MarkupLexerState
{
    Text,

    TagOpen,

    TagValue,

    TagClose
}
