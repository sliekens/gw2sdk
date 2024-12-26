using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Quaggans;

public class QuagganById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "present";

        var (actual, context) = await sut.Quaggans.GetQuagganById(id, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
