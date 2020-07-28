using System.Text.Json;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.TestCase
{
    public class MyItemReader : IJsonReader<MyItem>
    {
        private readonly JsonAggregateMapping<MyItem> _mapping = new JsonAggregateMapping<MyItem>
        {
            Name = "$"
        };

        public MyItemReader()
        {
            _mapping.UnexpectedPropertyBehavior = UnexpectedPropertyBehavior.Ignore;
            _mapping.Map("id", to => to.Id);
            _mapping.Map("level", to => to.Level);
            _mapping.Map(
                "details",
                details =>
                {
                    details.Map("vendor_value", to => to.VendorValue);
                },
                MappingSignificance.Required
            );
            //_el.Map("upgrade", to => to.Upgrade, new MyItemUpgradeReader());
        }

        public MyItem Read(in JsonElement json)
        {
            var compiler = new JsonReaderCompiler<MyItem>();

            var reader = compiler.Compile(_mapping);
            
            return reader(json);
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }
}