using System.Linq;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.MailCarriers;
using GW2SDK.Tests.Features.MailCarriers.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.MailCarriers
{
    public class MailCarrierReaderTest : IClassFixture<MailCarriersFixture>
    {
        public MailCarrierReaderTest(MailCarriersFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly MailCarriersFixture _fixture;

        private static class MailCarrierFact
        {
            public static void Id_is_positive(MailCarrier actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Non_default_carriers_can_be_unlocked(MailCarrier actual)
            {
                if (actual.Flags.SingleOrDefault() == MailCarrierFlag.Default)
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
            var sut = new MailCarrierReader();

            AssertEx.ForEach(_fixture.MailCarriers,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    MailCarrierFact.Id_is_positive(actual);
                    MailCarrierFact.Non_default_carriers_can_be_unlocked(actual);
                    MailCarrierFact.Order_is_not_negative(actual);
                    MailCarrierFact.Icon_is_not_empty(actual);
                    MailCarrierFact.Name_is_not_empty(actual);
                });
        }
    }
}
