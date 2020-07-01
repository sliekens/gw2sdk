using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Currencies.Fixture
{
    public class CurrencyFixture
    {
        public CurrencyFixture()
        {
            var reader = new JsonFlatFileReader();
            Currencies = reader.Read("Data/currencies.json").ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Currencies { get; }
    }
}
