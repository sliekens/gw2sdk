namespace GuildWars2;

/// <summary>A utility like String.Split, but for Spans. This should avoid allocations.</summary>
internal ref struct CharSpanSplitter(ReadOnlySpan<char> value, char separator)
{
    private ReadOnlySpan<char> value = value;

    public bool MoveNext(out ReadOnlySpan<char> result)
    {
        if (value == ReadOnlySpan<char>.Empty)
        {
            result = ReadOnlySpan<char>.Empty;
            return false;
        }

        var index = value.IndexOf(separator);
        if (index == -1)
        {
            result = value;
            value = ReadOnlySpan<char>.Empty;
            return true;
        }

        result = value[..index];
        value = value[(index + 1)..];
        return true;
    }
}
