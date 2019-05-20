using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Infrastructure.Colors;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Colors.Extensions
{
    public static class JsonColorServiceExtensions
    {
        public static async Task<ISet<string>> GetAllColorCategories(this ColorJsonService service)
        {
            var response = await service.GetAllColors();
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var root = JToken.Parse(json);
            var query = from value in root.SelectTokens("$[*].categories[*]", true).Cast<JValue>()
                orderby value.Value
                select (string) value.Value;

            return new HashSet<string>(query);
        }
    }
}
