using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce;

[ServiceDataSource]
public class DeliveryBox(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;

        (GuildWars2.Commerce.Delivery.DeliveryBox deliveryBox, MessageContext context) = await sut.Commerce.GetDeliveryBox(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        // Step through with debugger to see if the values reflect your in-game delivery box
        Assert.NotNull(context);
        Assert.NotNull(deliveryBox);
    }
}
