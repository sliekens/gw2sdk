using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

public class ObjectivesByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var (actual, context) = await sut.WizardsVault.GetObjectivesByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Count);
        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(context.ResultCount, pageSize);
    }
}
