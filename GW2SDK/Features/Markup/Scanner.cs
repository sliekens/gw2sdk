
using System.Diagnostics;

namespace GuildWars2.Markup;

internal class Scanner(string input)
{
    public int Position { get; private set; }

    public char Current => Position >= input.Length ? '\0' : input[Position];

    public bool CanAdvance => Position < input.Length;

    [DebuggerStepThrough]
    public char Peek() => Position + 1 >= input.Length ? '\0' : input[Position + 1];

    [DebuggerStepThrough]
    public void Advance() => Position++;
}
