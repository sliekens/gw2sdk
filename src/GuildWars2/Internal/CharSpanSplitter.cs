namespace GuildWars2;

/// <summary>A utility like String.Split, but for Spans. This should avoid allocations.</summary>
/// <param name="value">The span to split.</param>
/// <param name="separator">The character to split by.</param>
internal ref struct CharSpanSplitter(ReadOnlySpan<char> value, char separator)
{
    private ReadOnlySpan<char> value = value;

    public bool MoveNext(out ReadOnlySpan<char> result)
    {
        if (value == ReadOnlySpan<char>.Empty)
        {
            result = [];
            return false;
        }

        int index = value.IndexOf(separator);
        if (index == -1)
        {
            result = value;
            value = [];
            return true;
        }

        result = value[..index];
        value = value[(index + 1)..];
        return true;
    }
}
