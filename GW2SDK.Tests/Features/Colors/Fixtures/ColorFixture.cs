using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class ColorFixture
    {
        public ColorFixture()
        {
            var reader = new FlatFileReader();
            Colors = reader.Read("Data/colors.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> Colors { get; }
    }
}
