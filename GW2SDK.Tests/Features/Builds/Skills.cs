using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Builds;

public class Skills
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Builds.GetSkills();

        Assert.NotNull(actual.ResultContext);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }
}
