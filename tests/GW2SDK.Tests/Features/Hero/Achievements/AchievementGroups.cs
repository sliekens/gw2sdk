using System.Text.Json;

using GuildWars2.Hero.Achievements.Groups;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Achievements;

[ServiceDataSource]
public class AchievementGroups(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<AchievementGroup> actual, MessageContext context) = await sut.Hero.Achievements.GetAchievementGroups(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.NotEmpty(entry.Name);
            Assert.NotNull(entry.Description);
            Assert.True(entry.Order >= 0);
            Assert.NotEmpty(entry.Categories);
            Assert.All(entry.Categories, category => Assert.True(category > 0));
            string json;
            AchievementGroup? roundTrip;
#if NET
            json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.AchievementGroup);
            roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.AchievementGroup);
#else
            json = JsonSerializer.Serialize(entry);
            roundTrip = JsonSerializer.Deserialize<AchievementGroup>(json);
#endif
            Assert.Equal(entry, roundTrip);
        });
    }
}
