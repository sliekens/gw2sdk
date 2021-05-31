using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.MailCarriers.Fixtures
{
    public class MailCarriersFixture
    {
        public MailCarriersFixture()
        {
            var reader = new FlatFileReader();
            MailCarriers = reader.Read("Data/mailCarriers.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> MailCarriers { get; }
    }
}
