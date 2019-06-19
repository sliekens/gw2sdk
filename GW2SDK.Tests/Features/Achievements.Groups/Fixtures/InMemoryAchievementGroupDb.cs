using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Achievements.Groups.Fixtures
{
    public class InMemoryAchievementGroupDb
    {
        private readonly Dictionary<string, string> _db = new Dictionary<string, string>();

        public IReadOnlyList<string> Index => _db.Keys.ToList().AsReadOnly();

        public IReadOnlyList<string> AchievementCategories => _db.Values.ToList().AsReadOnly();

        public void AddAchievementCategory(string json)
        {
            var jobject = JObject.Parse(json);
            var id = (JValue) jobject.SelectToken("id");
            _db.Add(Convert.ToString(id.Value), json);
        }
    }
}
