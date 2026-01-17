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
        (IImmutableValueSet<AchievementGroup> actual, MessageContext context) = await sut.Hero.Achievements.GetAchievementGroups(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context)
            .Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
        foreach (AchievementGroup entry in actual)
        {
            await Assert.That(entry)
                .Member(e => e.Id, id => id.IsNotEmpty())
                .And.Member(e => e.Name, name => name.IsNotEmpty())
                .And.Member(e => e.Description, desc => desc.IsNotNull())
                .And.Member(e => e.Order, order => order.IsGreaterThanOrEqualTo(0))
                .And.Member(e => e.Categories, cats => cats.IsNotEmpty());
            foreach (int category in entry.Categories)
            {
                await Assert.That(category).IsGreaterThan(0);
            }

            string json;
            AchievementGroup? roundTrip;
#if NET
            json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.AchievementGroup);
            roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.AchievementGroup);
#else
            json = JsonSerializer.Serialize(entry);
            roundTrip = JsonSerializer.Deserialize<AchievementGroup>(json);
#endif
            await Assert.That(roundTrip).IsEqualTo(entry);
        }
    }
}
