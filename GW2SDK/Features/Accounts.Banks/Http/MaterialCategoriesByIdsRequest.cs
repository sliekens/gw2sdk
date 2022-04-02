using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Accounts.Banks.Http
{
    [PublicAPI]
    public sealed class MaterialCategoriesByIdsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/materials")
        {
            AcceptEncoding = "gzip"
        };

        public MaterialCategoriesByIdsRequest(IReadOnlyCollection<int> materialCategoriesIds, Language? language)
        {
            Check.Collection(materialCategoriesIds, nameof(materialCategoriesIds));
            MaterialCategoriesIds = materialCategoriesIds;
            Language = language;
        }

        public IReadOnlyCollection<int> MaterialCategoriesIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MaterialCategoriesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.MaterialCategoriesIds);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
