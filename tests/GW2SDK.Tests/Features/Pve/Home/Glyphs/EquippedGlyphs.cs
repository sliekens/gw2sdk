using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Glyphs;

public class EquippedGlyphs
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey token = TestConfiguration.ApiKey;
        (HashSet<string> actual, _) = await sut.Pve.Home.GetEquippedGlyphs(token.Key, TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.All(actual, Assert.NotEmpty);
    }
}
