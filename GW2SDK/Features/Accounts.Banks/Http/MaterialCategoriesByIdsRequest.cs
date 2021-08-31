using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks.Http
{
    [PublicAPI]
    public sealed class MaterialCategoriesByIdsRequest
    {
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
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/materials?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
