using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class Builds
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<BuildTemplate> actual, MessageContext context) = await sut.Hero.Builds.GetBuilds(
            character.Name,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context.Links);
        Assert.Equal(50, context.PageSize);
        Assert.Equal(1, context.PageTotal);
        Assert.Equal(context.ResultTotal, context.ResultCount);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotNull(entry);
                Assert.NotNull(entry.Build);
            }
        );
    }
}
