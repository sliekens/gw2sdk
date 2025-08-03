using GuildWars2.Pve.Pets;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Pets;

public class PetById
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        (Pet actual, MessageContext context) = await sut.Pve.Pets.GetPetById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
