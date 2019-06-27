using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Skins.Fixtures
{
    public class InMemorySkinDb
    {
        private readonly Dictionary<int, string> _db = new Dictionary<int, string>();

        public IReadOnlyList<int> Index => _db.Keys.ToList().AsReadOnly();

        public IReadOnlyList<string> Skins => _db.Values.ToList().AsReadOnly();

        public void AddSkin(string json)
        {
            var jobject = JObject.Parse(json);
            var id = (JValue) jobject.SelectToken("id");
            _db.Add(Convert.ToInt32(id.Value), json);
        }

        public IEnumerable<string> GetSkinFlags()
        {
            return (
                    from json in _db.Values
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("flags[*]")
                    select entries.Select(token => token.ToString())).SelectMany(values => values)
                                                                     .OrderBy(s => s)
                                                                     .Distinct();
        }

        public IEnumerable<string> GetSkinTypeNames()
        {
            return (
                    from json in _db.Values
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
            from json in _db.Values
            let jobject = JObject.Parse(json)
            where jobject.SelectToken("type").ToString() == typeName
            select jobject;

        public IEnumerable<string> GetSkinRarities()
        {
            return (
                    from json in _db.Values
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("rarity")
                    select entries.Select(token => token.ToString())).SelectMany(values => values)
                                                                     .OrderBy(s => s)
                                                                     .Distinct();
        }

        public IEnumerable<string> GetSkinRestrictions()
        {
            return (
                    from json in _db.Values
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
