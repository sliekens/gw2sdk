using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce;

public class DeliveryBox
{
    [Fact]
    public async Task Can_be_found()
    {
        var accessToken = TestConfiguration.ApiKey;
        var sut = Composer.Resolve<Gw2Client>();

        var (deliveryBox, context) = await sut.Commerce.GetDeliveryBox(accessToken.Key, cancellationToken: TestContext.Current.CancellationToken);

        // Step through with debugger to see if the values reflect your in-game delivery box
        Assert.NotNull(context);
        Assert.NotNull(deliveryBox);
    }
}
