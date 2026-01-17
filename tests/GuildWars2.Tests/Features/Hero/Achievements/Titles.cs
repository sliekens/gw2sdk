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
        (IImmutableValueSet<Title> actual, MessageContext context) = await sut.Hero.Achievements.GetTitles(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
        foreach (Title entry in actual)
        {
            if (entry.Id == 273)
            {
                // Invalid title?
                continue;
            }

            await Assert.That(entry.Id).IsGreaterThanOrEqualTo(1);
            await Assert.That(entry.Name).IsNotEmpty();
            MarkupSyntaxValidator.Validate(entry.Name);
            if (entry.AchievementPointsRequired.HasValue)
            {
                await Assert.That(entry.AchievementPointsRequired.Value).IsGreaterThanOrEqualTo(1).And.IsLessThanOrEqualTo(100000);
            }
            else
            {
                await Assert.That(entry.Achievements!).IsNotEmpty();
            }

#if NET
            string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.Title);
            Title? roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Title);
#else
            string json = JsonSerializer.Serialize(entry);
            Title? roundTrip = JsonSerializer.Deserialize<Title>(json);
#endif
            await Assert.That(roundTrip).IsEqualTo(entry);
        }
    }
}
