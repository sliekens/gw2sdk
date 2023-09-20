using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce;

public class DeliveryBox
{
    [Fact]
    public async Task Can_be_found()
    {
        var accessToken = Composer.Resolve<ApiKey>();
        var sut = Composer.Resolve<Gw2Client>();

        var deliveryBox = await sut.Commerce.GetDeliveryBox(accessToken.Key);

        // Step through with debugger to see if the values reflect your in-game delivery box
        Assert.NotNull(deliveryBox.Value);
    }
}
