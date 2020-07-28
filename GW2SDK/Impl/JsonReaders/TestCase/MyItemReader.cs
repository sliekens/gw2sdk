using System.Text.Json;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.TestCase
{
    public class MyItemReader : IJsonReader<MyItem>
    {
        private readonly JsonObjectMapping<MyItem> _mapping = new JsonObjectMapping<MyItem>
        {
            Name = "$"
        };

        public MyItemReader()
        {
            _mapping.Map("id", to => to.Id);
            _mapping.Map("level", to => to.Level);
            _mapping.Map(
                "details",
                details =>
                {
                    details.Map("vendor_value", to => to.VendorValue);
                }
            );
            _mapping.Map("upgrade", to => to.Upgrade, new MyItemUpgradeReader());
            _mapping.Map("price", to => to.Price,
                price =>
                {
                    price.Map("market", to => to.Market);
                    price.Ignore("new");
                });
        }

        public MyItem Read(in JsonElement json)
        {
            var compiler = new JsonMappingCompiler<MyItem>();

            var reader = compiler.Compile(_mapping);
            
            return reader(json);
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }
}