using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce;

public class DeliveryBox
{
    [Test]
    public async Task Can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (GuildWars2.Commerce.Delivery.DeliveryBox deliveryBox, MessageContext context) = await sut.Commerce.GetDeliveryBox(accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);
        // Step through with debugger to see if the values reflect your in-game delivery box
        Assert.NotNull(context);
        Assert.NotNull(deliveryBox);
    }
}
