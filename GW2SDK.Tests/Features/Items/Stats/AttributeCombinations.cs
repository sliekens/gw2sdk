using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items.Stats;

public class AttributeCombinations
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Items.GetAttributeCombinations(cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultTotal, actual.Count);
    }
}
