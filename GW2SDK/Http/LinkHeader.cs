using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace GuildWars2.Http;

[PublicAPI]
public sealed class LinkHeader
{
    public LinkHeader(IEnumerable<LinkHeaderValue> links)
    {
        Links = links?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(links));
    }

    public IReadOnlyCollection<LinkHeaderValue> Links { get; }

    public static LinkHeader Parse(string input) => new(ParseImpl(input.AsSpan()));

    private static IEnumerable<LinkHeaderValue> ParseImpl(ReadOnlySpan<char> input)
    {
        var splitter = new CharSpanSplitter(input, ',');
        var items = new List<LinkHeaderValue>();
        while (splitter.MoveNext())
        {
            if (!splitter.Current.IsEmpty)
            {
                items.Add(ParseLinkValue(splitter.Current));
            }
        }

        return items;
    }

    private static LinkHeaderValue ParseLinkValue(ReadOnlySpan<char> input)
    {
        string href = "", rel = "";
        var splitter = new CharSpanSplitter(input, ';');
        if (!splitter.MoveNext()) return new LinkHeaderValue(href, rel);
        href = ParseUri(splitter.Current);
        while (splitter.MoveNext())
        {
            var (name, value) = ParseAttribute(splitter.Current);
            if (name == "rel")
            {
                rel = value;
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
        var name = input.Slice(0, splitIndex).Trim();
        var value = input.Slice(splitIndex + 1).Trim();
        return (name: name.ToString(), value.ToString());
    }

    public override string ToString() => string.Join(", ", Links);
}
