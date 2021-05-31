using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Skins.Fixtures
{
    public class SkinFixture
    {
        public SkinFixture()
        {
            var reader = new FlatFileReader();
            Skins = reader.Read("Data/skins.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> Skins { get; }
    }
}
