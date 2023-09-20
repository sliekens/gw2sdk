using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Specializations;

public class SpecializationById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Specializations.GetSpecializationById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}
