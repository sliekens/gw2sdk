using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        private readonly List<string> _db = new List<string>();

        public IReadOnlyList<string> Worlds => _db.AsReadOnly();

        public void AddWorld(string json)
        {
            _db.Add(json);
        }

        public IImmutableList<int> GetIds() => (from entry in _db
            let jobject = JObject.Parse(entry)
            let id = (JValue) jobject.SelectToken("id")
            select Convert.ToInt32(id.Value)).ToImmutableList();

        public async Task LoadSnapshot()
        {
            using (var snapshot = File.Open("worlds.json", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            {
                if (snapshot.Length == 0)
                {
                    return;
                }

                using (var reader = new StreamReader(snapshot, Encoding.UTF8, false, 1024, true))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    jsonReader.SupportMultipleContent = true;
                    jsonReader.Read();
                    do
                    {
                        var json = await JObject.LoadAsync(jsonReader);
                        _db.Add(json.ToString(Formatting.None));
                    } while (jsonReader.Read());
                }
            }
        }

        public async Task CreateSnapshot()
        {
            using (var snapshot = File.Open("worlds.json", FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            using (var sw = new StreamWriter(snapshot, new UTF8Encoding(false)))
            using (var writer = new JsonTextWriter(sw))
            {
                foreach (var entry in _db) await writer.WriteRawAsync(entry);
            }
        }
    }
}
