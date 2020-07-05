using System.Linq;
using GW2SDK.MailCarriers;
using GW2SDK.Tests.Features.MailCarriers.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.MailCarriers
{
    public class MailCarrierTest : IClassFixture<MailCarriersFixture>
    {
        public MailCarrierTest(MailCarriersFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly MailCarriersFixture _fixture;

        private readonly ITestOutputHelper _output;

        private static class MailCarrierFact
        {
            public static void Id_is_positive(MailCarrier actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Non_default_carriers_can_be_unlocked(MailCarrier actual)
            {
                if (actual.flags.SingleOrDefault() == MailCarrierFlag.Default)
                {
                    Assert.Empty(actual.UnlockItems);
                }
                else
                {
                    Assert.NotEmpty(actual.UnlockItems);
                }
            }

            public static void Name_is_not_empty(MailCarrier actual) => Assert.NotEmpty(actual.Name);

            public static void Order_is_not_negative(MailCarrier actual) => Assert.InRange(actual.Order, 0, 1000);

            public static void Icon_is_not_empty(MailCarrier actual) => Assert.NotEmpty(actual.Icon);
        }

        [Fact]
        [Trait("Feature",    "MailCarriers")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void MailCarriers_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            AssertEx.ForEach(_fixture.MailCarriers,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<MailCarrier>(json, settings);
                    MailCarrierFact.Id_is_positive(actual);
                    MailCarrierFact.Non_default_carriers_can_be_unlocked(actual);
                    MailCarrierFact.Order_is_not_negative(actual);
                    MailCarrierFact.Icon_is_not_empty(actual);
                    MailCarrierFact.Name_is_not_empty(actual);
                });
        }
    }
}
