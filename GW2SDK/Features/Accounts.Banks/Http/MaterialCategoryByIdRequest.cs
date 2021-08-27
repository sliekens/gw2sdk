using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks.Http
{
    [PublicAPI]
    public sealed class MaterialCategoryByIdRequest
    {
        public MaterialCategoryByIdRequest(int materialCategoryId, Language? language)
        {
            MaterialCategoryId = materialCategoryId;
            Language = language;
        }

        public int MaterialCategoryId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MaterialCategoryByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.MaterialCategoryId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/materials?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
