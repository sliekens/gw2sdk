using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK;

/// <summary>A GET request for a given hyperlink.</summary>
/// <remarks>Not meant to be used directly.</remarks>
[PublicAPI]
public sealed class PageRequest
{
    public PageRequest(HyperlinkReference href, Language? language)
    {
        if (href is null or { IsEmpty: true })
        {
            throw new ArgumentException("The hyperlink cannot be empty.", nameof(href));
        }

        Href = href;
        Language = language;
    }

    public HyperlinkReference Href { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(PageRequest r)
    {
        HttpRequestMessageTemplate template = new(HttpMethod.Get, r.Href.Url)
        {
            AcceptEncoding = "gzip",
            AcceptLanguage = r.Language?.Alpha2Code
        };
        return template.Compile();
    }
}
