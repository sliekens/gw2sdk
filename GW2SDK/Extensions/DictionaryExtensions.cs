using System;
using System.Collections.Generic;
using GW2SDK.Features.Common;
using GW2SDK.Features.Common.Infrastructure;
using GW2SDK.Infrastructure;

namespace GW2SDK.Extensions
{
    public static class DictionaryExtensions
    {
        public static IListMetaData GetListMetaData([NotNull] this IDictionary<string, string> instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            return new ListMetaData
            {
                ResultTotal = int.Parse(instance[ResponseHeaderName.ResultTotal]),
                ResultCount = int.Parse(instance[ResponseHeaderName.ResultCount])
            };
        }

        public static IPagedListMetaData GetPagedListMetaData([NotNull] this IDictionary<string, string> instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            return new PagedListMetaData
            {
                PageTotal = int.Parse(instance[ResponseHeaderName.PageTotal]),
                PageSize = int.Parse(instance[ResponseHeaderName.PageSize]),
                ResultTotal = int.Parse(instance[ResponseHeaderName.ResultTotal]),
                ResultCount = int.Parse(instance[ResponseHeaderName.ResultCount])
            };
        }
    }
}
