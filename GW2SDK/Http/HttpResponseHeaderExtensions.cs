using System;
using System.Linq;
using System.Net.Http.Headers;
using GW2SDK.Impl;

namespace GW2SDK.Http
{
    public static class HttpResponseHeaderExtensions
    {
        public static ICollectionContext GetCollectionContext(this HttpResponseHeaders instance)
        {
            if (instance is null) throw new ArgumentNullException(nameof(instance));
            int resultTotal = default,
                resultCount = default;
            if (instance.TryGetValues(ResponseHeaderName.ResultTotal, out var resultTotals))
            {
                // Assume that there is exactly one value for this header
                resultTotal = int.Parse(resultTotals.Single());
            }

            if (instance.TryGetValues(ResponseHeaderName.ResultCount, out var resultCounts))
            {
                // Assume that there is exactly one value for this header
                resultCount = int.Parse(resultCounts.Single());
            }

            return new CollectionContext(resultTotal, resultCount);
        }

        public static IPageContext GetPageContext(this HttpResponseHeaders instance)
        {
            if (instance is null) throw new ArgumentNullException(nameof(instance));
            int pageTotal = default,
                pageSize = default,
                resultTotal = default,
                resultCount = default;
            ContinuationToken? previous = default,
                next = default,
                self = default,
                first = default,
                last = default;
            if (instance.TryGetValues(ResponseHeaderName.PageTotal, out var pageTotals))
            {
                // Assume that there is exactly one value for this header
                pageTotal = int.Parse(pageTotals.Single());
            }

            if (instance.TryGetValues(ResponseHeaderName.PageSize, out var pageSizes))
            {
                // Assume that there is exactly one value for this header
                pageSize = int.Parse(pageSizes.Single());
            }

            if (instance.TryGetValues(ResponseHeaderName.ResultTotal, out var resultTotals))
            {
                // Assume that there is exactly one value for this header
                resultTotal = int.Parse(resultTotals.Single());
            }

            if (instance.TryGetValues(ResponseHeaderName.ResultCount, out var resultCounts))
            {
                // Assume that there is exactly one value for this header
                resultCount = int.Parse(resultCounts.Single());
            }

            if (instance.TryGetValues(ResponseHeaderName.Link, out var links))
            {
                // Assume that there is exactly one value for this header
                var header = LinkHeader.Parse(links.Single());
                foreach (var link in header.Links)
                {
                    var continuationToken = new ContinuationToken(link.Href);
                    switch (link.Rel)
                    {
                        case "previous":
                            previous = continuationToken;
                            break;
                        case "next":
                            next = continuationToken;
                            break;
                        case "self":
                            self = continuationToken;
                            break;
                        case "first":
                            first = continuationToken;
                            break;
                        case "last":
                            last = continuationToken;
                            break;
                    }
                }
            }

            if (first is null || last is null || self is null)
            {
                throw new Exception();
            }

            return new PageContext(resultTotal, resultCount, pageTotal, pageSize, first, self, last, previous, next);
        }

        private static class ResponseHeaderName
        {
            internal const string Link = "Link";

            internal const string PageTotal = "X-Page-Total";

            internal const string PageSize = "X-Page-Size";

            internal const string ResultTotal = "X-Result-Total";

            internal const string ResultCount = "X-Result-Count";
        }
    }
}
