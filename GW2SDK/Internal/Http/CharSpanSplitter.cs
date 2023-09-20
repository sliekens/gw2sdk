namespace GuildWars2.Http;

/// <summary>An utility like String.Split, but for Spans. This should avoid allocations.</summary>
internal ref struct CharSpanSplitter
{
    private ReadOnlySpan<char> value;

    private readonly char separator;

    public CharSpanSplitter(ReadOnlySpan<char> value, char separator)
    {
        this.value = value;
        this.separator = separator;
        Current = ReadOnlySpan<char>.Empty;
    }

    public ReadOnlySpan<char> Current { get; private set; }

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

        Current = value.Slice(0, index);
        value = value.Slice(index + 1);
        return true;
    }
}
