using System.Text.Json;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.TestCase
{
    public class MyItemReader : IJsonReader<MyItem>
    {
        private readonly JsonAggregateMapping<MyItem> _el = new JsonAggregateMapping<MyItem>
        {
            Name = "RootObject"
        };

        public MyItemReader()
        {
            _el.Map("id", to => to.Id);
            _el.Map("level", to => to.Level);
            //_el.Map(
            //    "details",
            //    details =>
            //    {
            //        details.Map("vendor_value", to => to.VendorValue);
            //    }
            //);
            //_el.Map("upgrade", to => to.Upgrade, new MyItemUpgradeReader());
        }

        public MyItem Read(in JsonElement json)
        {
            var visitor = new JsonReaderCompiler<MyItem>
            {
                UnexpectedPropertyBehavior = UnexpectedPropertyBehavior.Ignore
            };
            _el.Accept(visitor);
            
            var reader = visitor.Source!.Compile();
            return reader(json);
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }
}