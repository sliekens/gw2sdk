using GuildWars2.Pve.Home.Cats;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

public class CatById
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int id = 20;
        (Cat actual, MessageContext context) = await sut.Pve.Home.GetCatById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
        Assert.Equal("necromancer", actual.Hint);
    }
}
