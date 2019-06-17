using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Achievements.Categories.Fixtures
{
    public class InMemoryAchievementCategoryDb
    {
        private readonly Dictionary<int, string> _db = new Dictionary<int, string>();

        public IReadOnlyList<int> Index => _db.Keys.ToList().AsReadOnly();

        public IReadOnlyList<string> AchievementCategories => _db.Values.ToList().AsReadOnly();

        public void AddAchievementCategory(string json)
        {
            var jobject = JObject.Parse(json);
            var id = (JValue) jobject.SelectToken("id");
            _db.Add(Convert.ToInt32(id.Value), json);
        }
    }
}
