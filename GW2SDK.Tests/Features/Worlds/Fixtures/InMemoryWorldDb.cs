using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        public async Task LoadSnapshot()
        {
            using (var snapshot = File.Open("worlds.json", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            {
                if (snapshot.Length == 0) return;

                using (var reader = new StreamReader(snapshot, Encoding.UTF8, false, 1024, true))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    jsonReader.SupportMultipleContent = true;
                    while (jsonReader.Read())
                    {
                        var json = await JObject.LoadAsync(jsonReader);
                        AddWorld(json.ToString(Formatting.None));
                    }
                }
            }
        }

        public async Task CreateSnapshot()
        {
            using (var snapshot = File.Open("worlds.json", FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            using (var sw = new StreamWriter(snapshot, new UTF8Encoding(false)))
            using (var writer = new JsonTextWriter(sw))
            {
                foreach (var entry in Worlds)
                {
                    await writer.WriteRawAsync(entry);
                }
            }
        }
    }
}
