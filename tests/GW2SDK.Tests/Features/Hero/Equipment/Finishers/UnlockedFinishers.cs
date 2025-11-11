using System.Text.Json;

using GuildWars2.Hero.Equipment.Finishers;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.Finishers;

[ServiceDataSource]
public class UnlockedFinishers(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<UnlockedFinisher> actual, _) = await sut.Hero.Equipment.Finishers.GetUnlockedFinishers(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
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

            string json = JsonSerializer.Serialize(entry);
            UnlockedFinisher? roundTrip = JsonSerializer.Deserialize<UnlockedFinisher>(json);
            Assert.Equal(entry, roundTrip);
        });
    }
}
