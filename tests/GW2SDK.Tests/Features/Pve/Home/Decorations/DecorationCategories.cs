using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

[ServiceDataSource]
public class DecorationCategories(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<DecorationCategory> actual, MessageContext context) = await sut.Pve.Home.GetDecorationCategories(cancellationToken: TestContext.Current!.Execution.CancellationToken);
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
