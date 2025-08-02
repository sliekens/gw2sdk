using System.Text.Json;

using GuildWars2.Hero.Equipment.Finishers;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Finishers;

public class UnlockedFinishers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Equipment.Finishers.GetUnlockedFinishers(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

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

                var json = JsonSerializer.Serialize(entry);
                var roundTrip = JsonSerializer.Deserialize<UnlockedFinisher>(json);
                Assert.Equal(entry, roundTrip);
            }
        );
    }
}
