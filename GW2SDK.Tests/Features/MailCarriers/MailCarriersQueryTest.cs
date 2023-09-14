using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.MailCarriers;

public class MailCarriersQueryTest
{


    [Fact]
    public async Task Mail_carriers_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.MailCarriers.GetMailCarriers();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            mailCarrier =>
            {
                mailCarrier.Id_is_positive();
                mailCarrier.Non_default_carriers_can_be_unlocked();
                mailCarrier.Order_is_not_negative();
                mailCarrier.Icon_is_not_empty();
                mailCarrier.Name_is_not_empty();
            }
        );
    }

    [Fact]
    public async Task Mail_carriers_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.MailCarriers.GetMailCarriersIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_mail_carrier_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.MailCarriers.GetMailCarrierById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Mail_carriers_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.MailCarriers.GetMailCarriersByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Mail_carriers_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.MailCarriers.GetMailCarriersByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Owned_mail_carriers_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.MailCarriers.GetOwnedMailCarriers(accessToken.Key);

        Assert.NotEmpty(actual.Value);

        var carriers = await sut.MailCarriers.GetMailCarriersByIds(actual.Value);

        Assert.Equal(actual.Value.Count, carriers.Value.Count);
        Assert.All(carriers.Value, carrier => Assert.Contains(carrier.Id, actual.Value));
    }
}
