using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Finishers;

public class UnlockedFinishers
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Finishers.GetUnlockedFinishers(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(
            actual.Value,
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
