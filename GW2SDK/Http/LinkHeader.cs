namespace GuildWars2.Http;

[PublicAPI]
public sealed class LinkHeader(IEnumerable<LinkHeaderValue> links)
{
    public IReadOnlyCollection<LinkHeaderValue> Links { get; } = links?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(links));

    public static LinkHeader Parse(string input) => new(ParseImpl(input.AsSpan()));

    private static IEnumerable<LinkHeaderValue> ParseImpl(ReadOnlySpan<char> input)
    {
        var splitter = new CharSpanSplitter(input, ',');
        var items = new List<LinkHeaderValue>();
        while (splitter.MoveNext(out var result))
        {
            items.Add(ParseLinkValue(result));
        }

        return items;
    }

    private static LinkHeaderValue ParseLinkValue(ReadOnlySpan<char> input)
    {
        string href = "", rel = "";
        var splitter = new CharSpanSplitter(input, ';');
        if (splitter.MoveNext(out var result))
        {
            href = ParseUri(result);
            while (splitter.MoveNext(out result))
            {
                var (name, value) = ParseAttribute(result);
                if (name == "rel")
                {
                    rel = value;
                }
            }
        }

        return new LinkHeaderValue(href, rel);
    }

    private static string ParseUri(ReadOnlySpan<char> input)
    {
        var startIndex = input.IndexOf('<') + 1;
        var length = input.IndexOf('>') - startIndex;
        return input.Slice(startIndex, length).ToString();
    }

    private static (string name, string value) ParseAttribute(ReadOnlySpan<char> input)
    {
        var splitIndex = input.IndexOf('=');
        var name = input[..splitIndex].Trim();
        var value = input[(splitIndex + 1)..].Trim();
        return (name: name.ToString(), value.ToString());
    }

    public override string ToString() => string.Join(", ", Links);
}
