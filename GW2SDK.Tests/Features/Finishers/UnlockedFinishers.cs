using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Finishers;

public class UnlockedFinishers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Finishers.GetUnlockedFinishers(accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                if (entry.Permanent)
                {
                    Assert.Null(entry.Quantity);
                }
                else
                {
                    Assert.True(entry.Quantity >= 0);
                }
            }
        );
    }
}
