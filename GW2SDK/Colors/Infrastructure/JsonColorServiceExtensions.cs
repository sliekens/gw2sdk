using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GW2SDK.Colors.Infrastructure
{
    public static class JsonColorServiceExtensions
    {
        public static async Task<List<string>> GetAllColorCategories(this JsonColorService service)
        {
            var json = await service.GetAllColors();
            var root = JToken.Parse(json);
            var query = from value in root.SelectTokens("$[*].categories[*]", true).Cast<JValue>()
                        orderby value.Value ascending
                        select (string)value.Value;

            return query.Distinct().ToList();
        }
    }
}
