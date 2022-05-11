using System;
using System.Linq;
using System.Net.Http.Headers;
using JetBrains.Annotations;
using static GW2SDK.Http.Gw2ResponseHeaderName;

namespace GW2SDK.Http;

[PublicAPI]
public static class HttpResponseHeaderExtensions
{
    public static ICollectionContext GetCollectionContext(this HttpResponseHeaders instance)
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        int resultTotal = default, resultCount = default;
        if (instance.TryGetValues(ResultTotal, out var resultTotals))
        {
            // Assume that there is exactly one value for this header
            resultTotal = int.Parse(resultTotals.Single());
        }

        if (instance.TryGetValues(ResultCount, out var resultCounts))
        {
            // Assume that there is exactly one value for this header
            resultCount = int.Parse(resultCounts.Single());
        }

        return new CollectionContext(resultTotal, resultCount);
    }

    public static IPageContext GetPageContext(this HttpResponseHeaders instance)
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        int pageTotal = default, pageSize = default, resultTotal = default, resultCount = default;
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

        if (instance.TryGetValues(PageSize, out var pageSizes))
        {
            // Assume that there is exactly one value for this header
            pageSize = int.Parse(pageSizes.Single());
        }

        if (instance.TryGetValues(ResultTotal, out var resultTotals))
        {
            // Assume that there is exactly one value for this header
            resultTotal = int.Parse(resultTotals.Single());
        }

        if (instance.TryGetValues(ResultCount, out var resultCounts))
        {
            // Assume that there is exactly one value for this header
            resultCount = int.Parse(resultCounts.Single());
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

        if (first is null || last is null || self is null)
        {
            throw new Exception();
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
