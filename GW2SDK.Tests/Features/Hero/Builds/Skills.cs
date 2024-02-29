using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class Skills
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Builds.GetSkills();

        Assert.Equal(actual.Count, context.ResultTotal);

        Assert.All(
            actual,
            skill =>
            {
                Assert.Empty(skill.SkillFlags.Other);

                var chatLink = skill.GetChatLink();
                Assert.Equal(skill.Id, chatLink.SkillId);
            }
        );
    }
}
