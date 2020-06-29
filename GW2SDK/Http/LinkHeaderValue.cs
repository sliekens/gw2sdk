using System;

namespace GW2SDK.Http
{
    internal sealed class LinkHeaderValue
    {
        internal LinkHeaderValue(string href, string rel)
        {
            if (string.IsNullOrWhiteSpace(href)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(href));
            if (string.IsNullOrWhiteSpace(rel)) throw new ArgumentException("Value cannot be null or whitespace.",  nameof(rel));
            Href = href;
            Rel = rel;
        }

        internal string Href { get; }

        internal string Rel { get; }

        public override string ToString() => $"<{Href}>; rel={Rel}";
    }
}
