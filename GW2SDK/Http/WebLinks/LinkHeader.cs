namespace GuildWars2.Http.WebLinks;

/// <summary>Represents web links in Link header values.</summary>
/// <remarks>The Link header is defined in RFC 8288: Web Linking.</remarks>
/// <param name="links">The collection of web links.</param>
[PublicAPI]
public sealed class LinkHeader(IEnumerable<LinkValue> links)
{
    /// <summary>Gets the web links.</summary>
    public IReadOnlyCollection<LinkValue> Links { get; } =
        Ensure.NotNull(links).ToList().AsReadOnly();

    /// <summary>Parses a Link header value.</summary>
    /// <param name="input">The Link header value to parse.</param>
    /// <returns>A <see cref="LinkHeader" /> instance.</returns>
    public static LinkHeader Parse(string input)
    {
        CharSpanSplitter splitter = new(input.AsSpan(), ',');
        List<LinkValue> items = [];
        while (splitter.MoveNext(out var result))
        {
            items.Add(ParseLinkValue(result));
        }

        return new LinkHeader(items);

        static LinkValue ParseLinkValue(in ReadOnlySpan<char> input)
        {
            string href = "", rel = "";
            CharSpanSplitter splitter = new(input, ';');
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

            return new LinkValue(href, rel);

            static string ParseUri(in ReadOnlySpan<char> input)
            {
                var startIndex = input.IndexOf('<') + 1;
                var length = input.IndexOf('>') - startIndex;
                return input.Slice(startIndex, length).ToString();
            }

            static (string name, string value) ParseAttribute(in ReadOnlySpan<char> input)
            {
                var splitIndex = input.IndexOf('=');
                var name = input[..splitIndex].Trim();
                var value = input[(splitIndex + 1)..].Trim();
                return (name: name.ToString(), value.ToString());
            }
        }
    }

    /// <summary>Returns the Link header value as a string.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return string.Join(", ", Links);
    }
}
