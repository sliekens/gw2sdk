using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Recipes.Fixtures
{
    public class InMemoryRecipeDb
    {
        public InMemoryRecipeDb(IEnumerable<string> objects)
        {
            Recipes = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Recipes { get; }

        public IEnumerable<string> GetRecipeFlags()
        {
            return (
                    from json in Recipes
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("flags[*]")
                    select entries.Select(token => token.ToString())).SelectMany(values => values)
                                                                     .OrderBy(s => s)
                                                                     .Distinct();
        }

        public IEnumerable<string> GetRecipeTypeNames()
        {
            return (
                    from json in Recipes
                    let jobject = JObject.Parse(json)
                    select jobject.SelectToken("type").ToString()).OrderBy(s => s)
                                                                  .Distinct();
        }

        public IEnumerable<string> GetRecipeDisciplines() =>
            (
                from json in Recipes
                let jobject = JObject.Parse(json)
                let entries = jobject.SelectTokens("disciplines[*]")
                select entries.Select(token => token.ToString())).SelectMany(values => values)
                                                                 .OrderBy(s => s)
                                                                 .Distinct();
    }
}
