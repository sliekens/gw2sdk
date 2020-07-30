namespace GW2SDK.Impl.JsonReaders.TestCase
{
    public class MyItemReader : JsonObjectReader<MyItem>
    {
        public MyItemReader()
        {
            Configure(
                mapping =>
                {
                    mapping.Map("id",    to => to.Id);
                    mapping.Map("level", to => to.Level);
                    mapping.Map(
                        "details",
                        details =>
                        {
                            details.Map("vendor_value", to => to.VendorValue);
                        }
                    );
                    mapping.Map("upgrade", to => to.Upgrade, new MyItemUpgradeReader());
                    mapping.Map(
                        "price",
                        to => to.Price,
                        price =>
                        {
                            price.Map("market", to => to.Market);
                            price.Ignore("new");
                        }
                    );
                }
            );
        }
    }
}
