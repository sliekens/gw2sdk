using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class InMemoryColorDb
    {
        public InMemoryColorDb(IEnumerable<string> objects)
        {
            Colors = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Colors { get; }

        public IEnumerable<string> GetColorCategoryNames()
        {
            return (
                    from json in Colors
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("categories[*]")
                    select flags.Select(token => token.ToString()))
                .SelectMany(entries => entries)
                .Distinct();
        }
    }
}
