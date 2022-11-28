using System;
using JetBrains.Annotations;

namespace GuildWars2.Http;

[PublicAPI]
public sealed class LinkHeaderValue
{
    public LinkHeaderValue(string href, string rel)
    {
        if (string.IsNullOrWhiteSpace(href))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(href));
        }

        if (string.IsNullOrWhiteSpace(rel))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(rel));
        }

        Href = href;
        Rel = rel;
    }

    public string Href { get; }

    public string Rel { get; }

    public override string ToString() => $"<{Href}>; rel={Rel}";
}
