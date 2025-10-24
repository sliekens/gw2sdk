using GuildWars2.Pve.Pets;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Pve.Pets;

public class Pets
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<Pet> actual, MessageContext context) = await sut.Pve.Pets.GetPets(cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotEmpty(actual);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.NotEmpty(entry.Description);
            Assert.NotNull(entry.IconUrl);
            Assert.NotEmpty(entry.Skills);
        });
    }
}
