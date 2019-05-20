using System;
using System.Collections.Generic;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Common;

namespace GW2SDK.Extensions
{
    public static class DictionaryExtensions
    {
        public static IListContext GetListContext([NotNull] this IDictionary<string, string> instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            var resultTotal = int.Parse(instance[ResponseHeaderName.ResultTotal]);
            var resultCount = int.Parse(instance[ResponseHeaderName.ResultCount]);
            return new ListContext(resultTotal, resultCount);
        }

        public static IPageContext GetPageContext([NotNull] this IDictionary<string, string> instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            var pageTotal = int.Parse(instance[ResponseHeaderName.PageTotal]);
            var pageSize = int.Parse(instance[ResponseHeaderName.PageSize]);
            var resultTotal = int.Parse(instance[ResponseHeaderName.ResultTotal]);
            var resultCount = int.Parse(instance[ResponseHeaderName.ResultCount]);
            return new PageContext(resultTotal, resultCount, pageTotal, pageSize);
        }
    }
}
