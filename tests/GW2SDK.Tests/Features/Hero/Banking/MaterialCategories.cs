using GuildWars2.Hero.Banking;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Banking;

[ServiceDataSource]
public class MaterialCategories(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<MaterialCategory> actual, MessageContext context) = await sut.Hero.Bank.GetMaterialCategories(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.NotEmpty(entry.Items);
            Assert.All(entry.Items, item => Assert.True(item > 0));
            Assert.True(entry.Order >= 0);
        });
    }
}
