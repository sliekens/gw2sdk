using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class ColorFixture
    {
        public ColorFixture()
        {
            var reader = new JsonFlatFileReader();
            Db = new InMemoryColorDb(reader.Read("Data/colors.json"));
        }

        public InMemoryColorDb Db { get; }
    }
}
