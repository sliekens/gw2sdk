using System;
using System.Linq;
using System.Net.Http.Headers;
using JetBrains.Annotations;
using static GuildWars2.Http.Gw2ResponseHeaderName;

namespace GuildWars2.Http;

[PublicAPI]
public static class HttpResponseHeaderExtensions
{
    public static ResultContext? GetResultContext(this HttpResponseHeaders instance)
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        int resultTotal, resultCount;
        if (instance.TryGetValues(ResultTotal, out var resultTotals))
        {
            // Assume that there is exactly one value for this header
            resultTotal = int.Parse(resultTotals.Single());
        }
        else
        {
            return null;
        }

        if (instance.TryGetValues(ResultCount, out var resultCounts))
        {
            // Assume that there is exactly one value for this header
            resultCount = int.Parse(resultCounts.Single());
        }
        else
        {
            return null;
        }

        return new ResultContext(resultTotal, resultCount);
    }

    public static PageContext? GetPageContext(this HttpResponseHeaders instance)
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        int pageTotal, pageSize, resultTotal, resultCount;
        Hyperlink previous = Hyperlink.None,
            next = Hyperlink.None,
            self = Hyperlink.None,
            first = Hyperlink.None,
            last = Hyperlink.None;
        if (instance.TryGetValues(PageTotal, out var pageTotals))
        {
            // Assume that there is exactly one value for this header
            pageTotal = int.Parse(pageTotals.Single());
        }
        else
        {
            return null;
        }

        if (instance.TryGetValues(PageSize, out var pageSizes))
        {
            // Assume that there is exactly one value for this header
            pageSize = int.Parse(pageSizes.Single());
        }
        else
        {
            return null;
        }

        if (instance.TryGetValues(ResultTotal, out var resultTotals))
        {
            // Assume that there is exactly one value for this header
            resultTotal = int.Parse(resultTotals.Single());
        }
        else
        {
            return null;
        }

        if (instance.TryGetValues(ResultCount, out var resultCounts))
        {
            // Assume that there is exactly one value for this header
            resultCount = int.Parse(resultCounts.Single());
        }
        else
        {
            return null;
        }

        if (instance.TryGetValues(Link, out var links))
        {
            // Assume that there is exactly one value for this header
            var header = LinkHeader.Parse(links.Single());
            foreach (var link in header.Links)
            {
                var href = new Hyperlink(link.Href);
                switch (link.Rel)
                {
                    case "previous":
                        previous = href;
                        break;
                    case "next":
                        next = href;
                        break;
                    case "self":
                        self = href;
                        break;
                    case "first":
                        first = href;
                        break;
                    case "last":
                        last = href;
                        break;
                }
            }
        }

        return new PageContext(
            resultTotal,
            resultCount,
            pageTotal,
            pageSize,
            first,
            self,
            last,
            previous,
            next
        );
    }
}
