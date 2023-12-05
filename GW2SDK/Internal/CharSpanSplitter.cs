namespace GuildWars2;

/// <summary>An utility like String.Split, but for Spans. This should avoid allocations.</summary>
internal ref struct CharSpanSplitter(ReadOnlySpan<char> value, char separator)
{
    public ReadOnlySpan<char> Current { get; private set; } = ReadOnlySpan<char>.Empty;

    public bool MoveNext()
    {
        if (value == ReadOnlySpan<char>.Empty)
        {
            return false;
        }

        var index = value.IndexOf(separator);
        if (index == -1)
        {
            Current = value;
            value = ReadOnlySpan<char>.Empty;
            return true;
        }

        Current = value[..index];
        value = value[(index + 1)..];
        return true;
    }
}
