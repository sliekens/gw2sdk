using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Skills;

public class Build
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        const int tab = 1;
        var actual = await sut.Skills.GetBuild(tab, character.Name, accessToken.Key);

        Assert.NotNull(actual.Value);
        Assert.NotNull(actual.Value.Build);
    }
}
