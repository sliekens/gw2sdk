using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class InMemoryColorDb
    {
        private readonly Dictionary<int, string> _db = new Dictionary<int, string>();

        public IReadOnlyList<int> Index => _db.Keys.ToList().AsReadOnly();

        public IReadOnlyList<string> Colors => _db.Values.ToList().AsReadOnly();

        public void AddColor(string json)
        {
            var jobject = JObject.Parse(json);
            var id = (JValue) jobject.SelectToken("id");
            _db.Add(Convert.ToInt32(id.Value), json);
        }

        public IEnumerable<string> GetColorCategoryNames()
        {
            return (
                    from json in _db.Values
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("categories[*]")
                    select flags.Select(token => token.ToString())).SelectMany(entries => entries)
                                                                   .Distinct();
        }

    }
}
