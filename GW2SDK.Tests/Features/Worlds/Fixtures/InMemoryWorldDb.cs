using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Worlds.Fixtures
{
    public class InMemoryWorldDb
    {
        private readonly Dictionary<int, string> _db = new Dictionary<int, string>();

        public IReadOnlyList<int> Index => _db.Keys.ToList().AsReadOnly();

        public IReadOnlyList<string> Worlds => _db.Values.ToList().AsReadOnly();

        public void AddWorld(string json)
        {
            var jobject = JObject.Parse(json);
            var id = (JValue) jobject.SelectToken("id");
            _db.Add(Convert.ToInt32(id.Value), json);
        }
    }
}
