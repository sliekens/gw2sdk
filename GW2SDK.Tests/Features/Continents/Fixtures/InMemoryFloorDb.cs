using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Continents.Fixtures
{
    public class InMemoryFloorDb
    {
        private readonly List<string> _db;

        public InMemoryFloorDb(IEnumerable<string> items)
        {
            _db = items.ToList();
        }

        public IReadOnlyCollection<string> Floors => _db.AsReadOnly();

        public IEnumerable<string> GetPointOfInterestTypeNames()
        {
            return (
                    from json in Floors
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("$.regions.*.maps.*.points_of_interest.*.type")
                    select flags.Select(token => token.ToString()))
                .SelectMany(flags => flags)
                .Distinct();
        }

        public IEnumerable<string> GetMasteryRegionNames()
        {
            return (
                    from json in Floors
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("$.regions.*.maps.*.mastery_points[*].region")
                    select flags.Select(token => token.ToString()))
                .SelectMany(types => types)
                .Distinct();
        }
    }
}
