using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Pets;

public class Pets
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Pve.Pets.GetPets(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotEmpty(entry.Name);
                Assert.NotEmpty(entry.Description);
                Assert.NotNull(entry.IconUrl);
                Assert.NotEmpty(entry.Skills);
            }
        );
    }
}
