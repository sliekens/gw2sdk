using GW2SDK.Tests.Shared;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class ColorFixture
    {
        public ColorFixture()
        {
            var reader = new JsonFlatFileReader();

            foreach (var color in reader.Read("Data/colors.json"))
            {
                Db.AddColor(color);
            }
        }

        public InMemoryColorDb Db { get; } = new InMemoryColorDb();
    }
}
