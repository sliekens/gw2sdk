using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.MailCarriers.Fixtures
{
    public class MailCarriersFixture
    {
        public MailCarriersFixture()
        {
            var reader = new JsonFlatFileReader();
            MailCarriers = reader.Read("Data/mailCarriers.json").ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> MailCarriers { get; }
    }
}
