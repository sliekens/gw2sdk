using GuildWars2.Hero;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class Characters
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<Character> actual, MessageContext context) = await sut.Hero.Account.GetCharacters(accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Name);
            Assert.True(entry.Race.IsDefined());
            Assert.True(entry.BodyType.IsDefined());
            Assert.True(entry.Profession.IsDefined());
            Assert.NotNull(entry.Flags);
            Assert.True(entry.Level > 0);
            Assert.True(entry.Age > TimeSpan.Zero);
            Assert.True(entry.LastModified > DateTimeOffset.MinValue);
            Assert.True(entry.Created > DateTimeOffset.MinValue);
            Assert.NotNull(entry.CraftingDisciplines);
            Assert.All(entry.CraftingDisciplines, discipline =>
            {
                Assert.True(discipline.Discipline.IsDefined());
            });
            Assert.NotNull(entry.Backstory);
            Assert.NotNull(entry.WvwAbilities);
            Assert.All(entry.WvwAbilities, ability =>
            {
                Assert.True(ability.Id > 0);
            });
            if (entry.BuildTemplates is not null)
            {
                Assert.All(entry.BuildTemplates, template =>
                {
                    Assert.NotNull(template.Build);
                    Assert.NotNull(template.Build.Skills);
                    Assert.NotNull(template.Build.AquaticSkills);
                    Assert.Equal(entry.Profession, template.Build.Profession);
                    if (template.Build.IsRangerBuild)
                    {
                        Assert.Equal(ProfessionName.Ranger, template.Build.Profession);
                    }
                    else if (template.Build.IsRevenantBuild)
                    {
                        Assert.Equal(ProfessionName.Revenant, template.Build.Profession);
                    }
                });
            }
        });
    }
}
