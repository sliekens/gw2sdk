using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.MailCarriers;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.MailCarriers;

public class MailCarrierQueryTest
{
    private static class MailCarrierFact
    {
        public static void Id_is_positive(MailCarrier actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);

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

        public static void Order_is_not_negative(MailCarrier actual) =>
            Assert.InRange(actual.Order, 0, 1000);

        public static void Icon_is_not_empty(MailCarrier actual) => Assert.NotEmpty(actual.Icon);
    }

    [Fact]
    public async Task Mail_carriers_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<MailCarrierQuery>();

        var actual = await sut.GetMailCarriers();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            mailCarrier =>
            {
                MailCarrierFact.Id_is_positive(mailCarrier);
                MailCarrierFact.Non_default_carriers_can_be_unlocked(mailCarrier);
                MailCarrierFact.Order_is_not_negative(mailCarrier);
                MailCarrierFact.Icon_is_not_empty(mailCarrier);
                MailCarrierFact.Name_is_not_empty(mailCarrier);
            }
            );
    }

    [Fact]
    public async Task Mail_carriers_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<MailCarrierQuery>();

        var actual = await sut.GetMailCarriersIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_mail_carrier_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MailCarrierQuery>();

        const int mailCarrierId = 1;

        var actual = await sut.GetMailCarrierById(mailCarrierId);

        Assert.Equal(mailCarrierId, actual.Value.Id);
    }

    [Fact]
    public async Task Mail_carriers_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MailCarrierQuery>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.GetMailCarriersByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
            );
    }

    [Fact]
    public async Task Mail_carriers_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<MailCarrierQuery>();

        var actual = await sut.GetMailCarriersByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task Owned_mail_carriers_can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<MailCarrierQuery>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.GetOwnedMailCarriers(accessToken.Key);

        Assert.NotEmpty(actual.Value);

        var carriers = await sut.GetMailCarriersByIds(actual.Value);

        Assert.Equal(actual.Value.Count, carriers.Count);
        Assert.All(carriers, carrier => Assert.Contains(carrier.Id, actual.Value));
    }
}
