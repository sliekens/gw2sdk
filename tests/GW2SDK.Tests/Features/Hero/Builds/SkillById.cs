using GuildWars2.Hero.Builds;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Builds;

public class SkillById
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 61533;

        (Skill actual, MessageContext context) = await sut.Hero.Builds.GetSkillById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
