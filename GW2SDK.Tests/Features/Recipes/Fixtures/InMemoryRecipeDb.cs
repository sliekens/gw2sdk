using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Recipes.Fixtures
{
    public class InMemoryRecipeDb
    {
        private readonly Dictionary<int, string> _db = new Dictionary<int, string>();

        public IReadOnlyList<int> Index => _db.Keys.ToList().AsReadOnly();

        public IReadOnlyList<string> Recipes => _db.Values.ToList().AsReadOnly();

        public void AddRecipe(string json)
        {
            var jobject = JObject.Parse(json);
            var id = (JValue) jobject.SelectToken("id");
            _db.Add(Convert.ToInt32(id.Value), json);
        }

        public IEnumerable<string> GetRecipeFlags()
        {
            return (
                    from json in _db.Values
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("flags[*]")
                    select entries.Select(token => token.ToString())).SelectMany(values => values)
                                                                     .OrderBy(s => s)
                                                                     .Distinct();
        }

        public IEnumerable<string> GetRecipeTypeNames()
        {
            return (
                    from json in _db.Values
                    let jobject = JObject.Parse(json)
                    select jobject.SelectToken("type").ToString()).OrderBy(s => s)
                                                                  .Distinct();
        }

        public IEnumerable<string> GetRecipeDisciplines() =>
            (
                from json in _db.Values
                let jobject = JObject.Parse(json)
                let entries = jobject.SelectTokens("disciplines[*]")
                select entries.Select(token => token.ToString())).SelectMany(values => values)
                                                                 .OrderBy(s => s)
                                                                 .Distinct();
    }
}
