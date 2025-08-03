using System.Text.Json;

using GuildWars2.Hero.Achievements.Groups;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AchievementGroups
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<AchievementGroup> actual, MessageContext context) =
            await sut.Hero.Achievements.GetAchievementGroups(
                cancellationToken: TestContext.Current.CancellationToken
            );

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.NotEmpty(entry.Name);
                Assert.NotNull(entry.Description);
                Assert.True(entry.Order >= 0);
                Assert.NotEmpty(entry.Categories);
                Assert.All(entry.Categories, category => Assert.True(category > 0));

                string json = JsonSerializer.Serialize(entry);
                AchievementGroup? roundTrip = JsonSerializer.Deserialize<AchievementGroup>(json);
                Assert.Equal(entry, roundTrip);
            }
        );
    }
}
