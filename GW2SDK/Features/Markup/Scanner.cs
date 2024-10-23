
namespace GuildWars2.Markup;

internal class Scanner(string input)
{
    private int position;

    public char Current => position >= input.Length ? '\0' : input[position];

    public bool CanAdvance => position < input.Length;

    public void Advance() => position++;

    public string ReadUntil(char c)
    {
        var start = position;
        while (Current != c && CanAdvance)
        {
            Advance();
        }

        return input[start..position];
    }

    public string ReadUntilAny(params char[] chars)
    {
        var start = position;
        while (!chars.Contains(Current) && CanAdvance)
        {
            Advance();
        }

        return input[start..position];
    }
}
