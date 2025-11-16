using System.Text.Json;

using GuildWars2.Hero.Achievements.Categories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Achievements;

[ServiceDataSource]
public class AchievementCategories(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<AchievementCategory> actual, MessageContext context) = await sut.Hero.Achievements.GetAchievementCategories(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context)
            .Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
        foreach (AchievementCategory entry in actual)
        {
            await Assert.That(entry)
                .Member(e => e.Id, m => m.IsGreaterThan(0))
                .And.Member(e => e.Name, m => m.IsNotEmpty())
                .And.Member(e => e.Description, m => m.IsNotNull())
                .And.Member(e => e.Order, m => m.IsGreaterThanOrEqualTo(0));
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref

            await Assert.That(entry.IconHref).IsNotEmpty();
#pragma warning restore CS0618
            await Assert.That(entry.IconUrl).IsNotNull()
                .And.Member(u => u.IsAbsoluteUri, m => m.IsTrue());
            await Assert.That(entry.Achievements).IsNotNull();
            foreach (AchievementRef achievement in entry.Achievements)
            {
                await Assert.That(achievement)
                    .Member(a => a.Id, m => m.IsGreaterThan(0))
                    .And.Member(a => a.Flags.Other, m => m.IsEmpty());
                if (achievement.Level is not null)
                {
                    await Assert.That(achievement.Level)
                        .Member(l => l.Min, min => min.IsGreaterThanOrEqualTo(1).And.IsLessThanOrEqualTo(80))
                        .And.Member(l => l.Max, max => max.IsGreaterThanOrEqualTo(1).And.IsLessThanOrEqualTo(80).And.IsGreaterThanOrEqualTo(achievement.Level.Min));
                }
            }

            if (entry.Tomorrow is not null)
            {
                foreach (AchievementRef achievement in entry.Tomorrow)
                {
                    await Assert.That(achievement)
                        .Member(a => a.Id, id => id.IsGreaterThan(0))
                        .And.Member(a => a.Flags.Other, other => other.IsEmpty());
                    if (achievement.Level is not null)
                    {
                        await Assert.That(achievement.Level)
                            .Member(l => l.Min, min => min.IsGreaterThanOrEqualTo(1).And.IsLessThanOrEqualTo(80))
                            .And.Member(l => l.Max, max => max.IsGreaterThanOrEqualTo(1).And.IsLessThanOrEqualTo(80).And.IsGreaterThanOrEqualTo(achievement.Level.Min));
                    }
                }
            }

#if NET
            string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.AchievementCategory);
            AchievementCategory? roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.AchievementCategory);
#else
            string json = JsonSerializer.Serialize(entry);
            AchievementCategory? roundTrip = JsonSerializer.Deserialize<AchievementCategory>(json);
#endif
            await Assert.That(roundTrip).IsEqualTo(entry);
        }
    }
}
