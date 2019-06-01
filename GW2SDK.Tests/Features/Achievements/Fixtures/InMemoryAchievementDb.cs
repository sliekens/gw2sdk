using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Achievements.Fixtures
{
    public class InMemoryAchievementDb
    {
        private readonly Dictionary<int, string> _db = new Dictionary<int, string>();

        public IReadOnlyList<int> Index => _db.Keys.ToList().AsReadOnly();

        public IReadOnlyList<string> Achievements => _db.Values.ToList().AsReadOnly();

        public void AddAchievement(string json)
        {
            var jobject = JObject.Parse(json);
            var id = (JValue) jobject.SelectToken("id");
            _db.Add(Convert.ToInt32(id.Value), json);
        }

        public IEnumerable<string> GetAchievementFlags()
        {
            return (
                    from json in _db.Values
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("flags[*]")
                    select flags.Select(token => token.ToString())).SelectMany(flags => flags)
                                                                   .Distinct();
        }

        public IEnumerable<string> GetAchievementTypeNames()
        {
            return (
                    from json in _db.Values
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("type")
                    select flags.Select(token => token.ToString())).SelectMany(types => types)
                                                                   .Distinct();
        }

        public IEnumerable<string> GetAchievementRewardTypeNames()
        {
            return (
                    from json in _db.Values
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("rewards[*].type")
                    select flags.Select(token => token.ToString())).SelectMany(types => types)
                                                                   .Distinct();
        }

        public IEnumerable<string> GetAchievementBitTypeNames()
        {
            return (
                    from json in _db.Values
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("bits[*].type")
                    select flags.Select(token => token.ToString())).SelectMany(types => types)
                                                                   .Distinct();
        }

        public IEnumerable<string> GetAchievementMasteryRegionNames()
        {
            return (
                    from json in _db.Values
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("rewards[*].region")
                    select flags.Select(token => token.ToString())).SelectMany(types => types)
                                                                   .Distinct();
        }
    }
}
