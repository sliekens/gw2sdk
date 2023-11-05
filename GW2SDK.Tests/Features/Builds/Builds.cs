using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Builds;

public class Builds
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Builds.GetBuilds(character.Name, accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
        {
            Assert.NotNull(entry);
            Assert.NotNull(entry.Build);
        });
    }
}
