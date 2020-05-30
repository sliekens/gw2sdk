using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Skins.Fixtures
{
    public class InMemorySkinDb
    {
        public InMemorySkinDb(IEnumerable<string> objects)
        {
            Skins = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Skins { get; }

        public IEnumerable<string> GetSkinFlags()
        {
            return (
                    from json in Skins
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("flags[*]")
                    select entries.Select(token => token.ToString())).SelectMany(values => values)
                                                                     .OrderBy(s => s)
                                                                     .Distinct();
        }

        public IEnumerable<string> GetSkinTypeNames()
        {
            return (
                    from json in Skins
                    let jobject = JObject.Parse(json)
                    select jobject.SelectToken("type").ToString()).OrderBy(s => s)
                                                                  .Distinct();
        }

        public IEnumerable<string> GetWeaponTypeNames() =>
            (
                from jobject in GetSkinsByType("Weapon")
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
                                                                        .Distinct();

        public IEnumerable<string> GetArmorTypeNames() =>
            (
                from jobject in GetSkinsByType("Armor")
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
                                                                        .Distinct();

        public IEnumerable<string> GetGatheringToolTypeNames() =>
            (
                from jobject in GetSkinsByType("Gathering")
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
                                                                        .Distinct();

        public IEnumerable<JObject> GetSkinsByType(string typeName) =>
            from json in Skins
            let jobject = JObject.Parse(json)
            where jobject.SelectToken("type").ToString() == typeName
            select jobject;

        public IEnumerable<string> GetSkinRarities()
        {
            return (
                    from json in Skins
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("rarity")
                    select entries.Select(token => token.ToString())).SelectMany(values => values)
                                                                     .OrderBy(s => s)
                                                                     .Distinct();
        }

        public IEnumerable<string> GetSkinRestrictions()
        {
            return (
                    from json in Skins
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("restrictions[*]")
                    select entries.Select(token => token.ToString())).SelectMany(values => values)
                                                                     .OrderBy(s => s)
                                                                     .Distinct();
        }

        public IEnumerable<string> GetWeaponDamageTypes() =>
            (
                from jobject in GetSkinsByType("Weapon")
                select jobject.SelectToken("$.details.damage_type").ToString()).OrderBy(s => s)
                                                                               .Distinct();

        public IEnumerable<string> GetArmorWeightClasses() =>
            (
                from jobject in GetSkinsByType("Armor")
                select jobject.SelectToken("$.details.weight_class").ToString()).OrderBy(s => s)
                                                                                .Distinct();
    }
}
