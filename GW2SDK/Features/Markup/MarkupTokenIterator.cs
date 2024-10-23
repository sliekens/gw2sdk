namespace GuildWars2.Markup;

internal class MarkupTokenIterator(IEnumerable<MarkupToken> input)
{
    private readonly List<MarkupToken> tokens = input.ToList();

    private int position;

    public MarkupToken? Current => position < tokens.Count ? tokens[position] : null;

    public void Advance() => position++;
}
