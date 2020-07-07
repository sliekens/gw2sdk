using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Traits.Fixtures
{
    public class TraitsFixture
    {
        public TraitsFixture()
        {
            var reader = new JsonFlatFileReader();
            Traits = reader.Read("Data/traits.json").ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Traits { get; }
    }
}
