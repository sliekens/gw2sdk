using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

public class Cats
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Pve.Home.GetCats(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            cat =>
            {
                Assert.NotNull(cat);
                Assert.True(cat.Id > 0);
                Assert.NotEmpty(cat.Hint);
            }
        );
    }
}
