using System.Net.Http.Headers;

using GuildWars2.Http.WebLinks;

using static GuildWars2.Http.Gw2ResponseHeaderName;

namespace GuildWars2.Http;

internal static class HttpResponseHeaderExtensions
{
    public static int? GetResultCount(this HttpResponseHeaders instance)
    {
        return instance.GetInt32(ResultCount);
    }

    public static int? GetResultTotal(this HttpResponseHeaders instance)
    {
        return instance.GetInt32(ResultTotal);
    }

    public static int? GetPageSize(this HttpResponseHeaders instance)
    {
        return instance.GetInt32(PageSize);
    }

    public static int? GetPageTotal(this HttpResponseHeaders instance)
    {
        return instance.GetInt32(PageTotal);
    }

    public static Link? GetLink(this HttpResponseHeaders instance)
    {
        Hyperlink previous = Hyperlink.None,
            next = Hyperlink.None,
            self = Hyperlink.None,
            first = Hyperlink.None,
            last = Hyperlink.None;
        if (instance.TryGetValues(Gw2ResponseHeaderName.Link, out IEnumerable<string>? values))
        {
            foreach (string? value in values)
            {
                LinkHeader header = LinkHeader.Parse(value);
                foreach (LinkValue link in header.Links)
                {
#pragma warning disable CS0618 // Suppress obsolete warning
                    Hyperlink href = new(link.Target);
#pragma warning restore CS0618
                    switch (link.RelationType)
                    {
                        case "previous" when previous.IsEmpty:
                            previous = href;
                            break;
                        case "next" when next.IsEmpty:
                            next = href;
                            break;
                        case "self" when self.IsEmpty:
                            self = href;
                            break;
                        case "first" when first.IsEmpty:
                            first = href;
                            break;
                        case "last" when last.IsEmpty:
                            last = href;
                            break;
                        default:
                            break;
                    }
                }
            }

            return new Link(first, self, last, previous, next);
        }

        return null;
    }

    private static int? GetInt32(this HttpResponseHeaders instance, string headerName)
    {
        if (instance.TryGetValues(headerName, out IEnumerable<string>? values))
        {
            foreach (string? value in values)
            {
                if (int.TryParse(value, out int int32))
                {
                    return int32;
                }
            }
        }

        return null;
    }
}
