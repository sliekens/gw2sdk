using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Builds;

public class ActiveBuild
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Builds.GetActiveBuild(character.Name, accessToken.Key);

        Assert.NotNull(actual.Value);
        Assert.NotNull(actual.Value.Build);
    }
}
