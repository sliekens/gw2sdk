using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

public class Skins
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        // You wouldn't want to use Take() in production code
        //   but enumerating all entries is too expensive for a test
        // This code will actually try to fetch more than 600 entries
        //  but the extra requests will be cancelled when this test completes
        await foreach (var (actual, context) in sut.Hero.Equipment.Wardrobe
            .GetSkinsBulk(degreeOfParallelism: 3)
            .Take(600))
        {
            actual.Has_id();
            Assert.NotNull(context);
        }
    }
}
