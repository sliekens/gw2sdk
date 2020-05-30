using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Achievements.Fixtures
{
    public class InMemoryAchievementDb
    {
        public InMemoryAchievementDb(IEnumerable<string> objects)
        {
            Achievements = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Achievements { get; }

        public IEnumerable<string> GetAchievementFlags()
        {
            return (
                    from json in Achievements
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("flags[*]")
                    select flags.Select(token => token.ToString())).SelectMany(flags => flags)
                                                                   .Distinct();
        }

        public IEnumerable<string> GetAchievementTypeNames()
        {
            return (
                    from json in Achievements
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("type")
                    select flags.Select(token => token.ToString())).SelectMany(types => types)
                                                                   .Distinct();
        }

        public IEnumerable<string> GetAchievementRewardTypeNames()
        {
            return (
                    from json in Achievements
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("rewards[*].type")
                    select flags.Select(token => token.ToString())).SelectMany(types => types)
                                                                   .Distinct();
        }

        public IEnumerable<string> GetAchievementBitTypeNames()
        {
            return (
                    from json in Achievements
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("bits[*].type")
                    select flags.Select(token => token.ToString())).SelectMany(types => types)
                                                                   .Distinct();
        }

        public IEnumerable<string> GetMasteryRegionNames()
        {
            return (
                    from json in Achievements
                    let jobject = JObject.Parse(json)
                    let flags = jobject.SelectTokens("rewards[*].region")
                    select flags.Select(token => token.ToString())).SelectMany(types => types)
                                                                   .Distinct();
        }
    }
}
