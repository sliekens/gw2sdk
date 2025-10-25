using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

public class DecorationCategories
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<DecorationCategory> actual, MessageContext context) = await sut.Pve.Home.GetDecorationCategories(cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, category =>
        {
            Assert.NotNull(category);
            Assert.True(category.Id > 0);
            Assert.NotEmpty(category.Name);
        });
    }
}
