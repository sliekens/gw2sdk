using System;
using System.Threading.Tasks;
using GW2SDK.MailCarriers;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.MailCarriers
{
    public class MailCarrierServiceTest
    {
        [Fact]
        [Trait("Feature",  "MailCarriers")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_mail_carriers()
        {
            await using var services = new Container();
            var sut = services.Resolve<MailCarrierService>();

            var actual = await sut.GetMailCarriers();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "MailCarriers")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_mail_carrier_ids()
        {
            await using var services = new Container();
            var sut = services.Resolve<MailCarrierService>();

            var actual = await sut.GetMailCarriersIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "MailCarriers")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_mail_carrier_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<MailCarrierService>();

            const int mailCarrierId = 1;

            var actual = await sut.GetMailCarrierById(mailCarrierId);

            Assert.Equal(mailCarrierId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "MailCarriers")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_mail_carriers_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<MailCarrierService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetMailCarriersByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        [Trait("Feature",  "MailCarriers")]
        [Trait("Category", "Unit")]
        public async Task Mail_carrier_ids_cannot_be_null()
        {
            await using var services = new Container();
            var sut = services.Resolve<MailCarrierService>();

            await Assert.ThrowsAsync<ArgumentNullException>("mailCarrierIds",
                async () =>
                {
                    await sut.GetMailCarriersByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "MailCarriers")]
        [Trait("Category", "Unit")]
        public async Task Mail_carrier_ids_cannot_be_empty()
        {
            await using var services = new Container();
            var sut = services.Resolve<MailCarrierService>();

            await Assert.ThrowsAsync<ArgumentException>("mailCarrierIds",
                async () =>
                {
                    await sut.GetMailCarriersByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "MailCarriers")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_mail_carriers_by_page()
        {
            await using var services = new Container();
            var sut = services.Resolve<MailCarrierService>();

            var actual = await sut.GetMailCarriersByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "MailCarriers")]
        [Trait("Category", "Integration")]
        public async Task Page_index_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<MailCarrierService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetMailCarriersByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "MailCarriers")]
        [Trait("Category", "Integration")]
        public async Task Page_size_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<MailCarrierService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetMailCarriersByPage(1, -3));
        }
    }
}
