using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace GW2SDK
{
    /// <summary>A GET request for a given hyperlink.</summary>
    /// <remarks>Not meant to be used directly.</remarks>
    [PublicAPI]
    public sealed class PageRequest
    {
        public PageRequest(HyperlinkReference href)
        {
            if (href is null or { IsEmpty: true })
            {
                throw new ArgumentException("The hyperlink cannot be empty.", nameof(href));
            }

            Href = href;
        }

        public HyperlinkReference Href { get; }

        public static implicit operator HttpRequestMessage(PageRequest r)
        {
            var location = new Uri(r.Href.Url, UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
