﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.MailCarriers;

public class MailCarriers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.MailCarriers.GetMailCarriers();

        Assert.NotNull(context.ResultContext);
        Assert.Equal(context.ResultContext.ResultTotal, actual.Count);
        Assert.All(
            actual,
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
}
