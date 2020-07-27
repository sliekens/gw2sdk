namespace GW2SDK.Impl.JsonReaders.TestCase
{
    public class MyItem
    {
        public int Id { get; set; }

        public int Level { get; set; }

        public int VendorValue { get; set; }

        public MyItemUpgrade Upgrade { get; set; } = new MyItemUpgrade();
    }
}