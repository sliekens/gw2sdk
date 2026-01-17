using GuildWars2.Hero;
using GuildWars2.Hero.Accounts;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Accounts;

[ServiceDataSource]
public class Characters(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (IImmutableValueSet<Character> actual, MessageContext context) = await sut.Hero.Account.GetCharacters(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context)
            .Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();

        using (Assert.Multiple())
        {
            foreach (Character entry in actual)
            {
                await Assert.That(entry)
                    .Member(e => e.Name, m => m.IsNotEmpty())
                    .And.Member(e => e.Race.IsDefined(), m => m.IsTrue())
                    .And.Member(e => e.BodyType.IsDefined(), m => m.IsTrue())
                    .And.Member(e => e.Profession.IsDefined(), m => m.IsTrue())
                    .And.Member(e => e.Flags, m => m.IsNotNull())
                    .And.Member(e => e.Level, m => m.IsGreaterThan(0))
                    .And.Member(e => e.Age, m => m.IsGreaterThan(TimeSpan.Zero))
                    .And.Member(e => e.LastModified, m => m.IsGreaterThan(DateTimeOffset.MinValue))
                    .And.Member(e => e.Created, m => m.IsGreaterThan(DateTimeOffset.MinValue))
                    .And.Member(e => e.CraftingDisciplines, m => m.IsNotNull());

                foreach (CraftingDiscipline discipline in entry.CraftingDisciplines)
                {
                    await Assert.That(discipline.Discipline.IsDefined()).IsTrue();
                }

                await Assert.That(entry.Backstory).IsNotNull();
                await Assert.That(entry.WvwAbilities).IsNotNull();

                if (entry.WvwAbilities is not null)
                {
                    foreach (WvwAbility ability in entry.WvwAbilities)
                    {
                        await Assert.That(ability.Id).IsGreaterThan(0);
                    }
                }

                if (entry.BuildTemplates is not null)
                {
                    foreach (BuildTemplate template in entry.BuildTemplates)
                    {
                        await Assert.That(template.Build)
                            .Member(b => b, m => m.IsNotNull())
                            .And.Member(b => b.Skills, m => m.IsNotNull())
                            .And.Member(b => b.AquaticSkills, m => m.IsNotNull())
                            .And.Member(b => b.Profession, m => m.IsEqualTo(entry.Profession));

                        if (template.Build.IsRangerBuild)
                        {
                            await Assert.That(template.Build.Profession).IsEqualTo(ProfessionName.Ranger);
                        }
                        else if (template.Build.IsRevenantBuild)
                        {
                            await Assert.That(template.Build.Profession).IsEqualTo(ProfessionName.Revenant);
                        }
                    }
                }
            }
        }
    }
}
