using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Traits;

public class Traits
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Traits.GetTraits();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            trait =>
            {
                trait.Id_is_positive();
            }
        );
    }
}
