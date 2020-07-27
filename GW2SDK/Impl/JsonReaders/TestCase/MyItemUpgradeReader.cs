using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders.TestCase
{
    public class MyItemUpgradeReader : IJsonReader<MyItemUpgrade>
    {
        public MyItemUpgrade Read(in JsonElement json) =>
            new MyItemUpgrade
            {
                Power = json.GetProperty("power").GetInt32()
            };

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }
}