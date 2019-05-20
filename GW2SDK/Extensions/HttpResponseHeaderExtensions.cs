using System;
using System.Net.Http.Headers;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Common;

namespace GW2SDK.Extensions
{
    public static class HttpResponseHeaderExtensions
    {
        public static IListContext GetListContext([NotNull] this HttpResponseHeaders instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            var resultTotal = int.Parse(instance.GetValues(ResponseHeaderName.ResultTotal).ToCsv());
            var resultCount = int.Parse(instance.GetValues(ResponseHeaderName.ResultCount).ToCsv());
            return new ListContext(resultTotal, resultCount);
        }

        public static IPageContext GetPageContext([NotNull] this HttpResponseHeaders instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            var pageTotal = int.Parse(instance.GetValues(ResponseHeaderName.PageTotal).ToCsv());
            var pageSize = int.Parse(instance.GetValues(ResponseHeaderName.PageSize).ToCsv());
            var resultTotal = int.Parse(instance.GetValues(ResponseHeaderName.ResultTotal).ToCsv());
            var resultCount = int.Parse(instance.GetValues(ResponseHeaderName.ResultCount).ToCsv());
            return new PageContext(resultTotal, resultCount, pageTotal, pageSize);
        }
    }
}
