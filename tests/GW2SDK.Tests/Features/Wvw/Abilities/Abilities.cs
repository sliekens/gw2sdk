using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Abilities;

namespace GuildWars2.Tests.Features.Wvw.Abilities;

public class Abilities
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Ability> actual, MessageContext context) = await sut.Wvw.GetAbilities(cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.NotEmpty(entry.Description);
#pragma warning disable CS0618 // IconHref is obsolete

            Assert.NotEmpty(entry.IconHref);
#pragma warning restore CS0618
            Assert.True(entry.IconUrl.IsAbsoluteUri);
            Assert.NotEmpty(entry.Ranks);
        });
    }
}
