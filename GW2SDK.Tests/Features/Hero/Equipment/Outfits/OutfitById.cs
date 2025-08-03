using GuildWars2.Hero.Equipment.Outfits;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Outfits;

public class OutfitById
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        (Outfit actual, MessageContext context) = await sut.Hero.Equipment.Outfits.GetOutfitById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
