using System.Text.Json;

using GuildWars2.Hero.Achievements.Titles;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Achievements;

[ServiceDataSource]
public class Titles(Gw2Client sut)
{
    [Test]
    public async Task Titles_can_be_listed()
    {
        (HashSet<Title> actual, MessageContext context) = await sut.Hero.Achievements.GetTitles(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
        {
            if (entry.Id == 273)
            {
                // Invalid title?
                return;
            }

            Assert.True(entry.Id >= 1);
            Assert.NotEmpty(entry.Name);
            MarkupSyntaxValidator.Validate(entry.Name);
            if (entry.AchievementPointsRequired.HasValue)
            {
                Assert.InRange(entry.AchievementPointsRequired.Value, 1, 100000);
            }
            else
            {
                Assert.NotEmpty(entry.Achievements!);
            }

            string json = JsonSerializer.Serialize(entry);
            Title? roundTrip = JsonSerializer.Deserialize<Title>(json);
            Assert.Equal(entry, roundTrip);
        });
    }
}
