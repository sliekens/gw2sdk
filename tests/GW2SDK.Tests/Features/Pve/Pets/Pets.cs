using GuildWars2.Pve.Pets;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Pets;

[ServiceDataSource]
public class Pets(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Pet> actual, MessageContext context) = await sut.Pve.Pets.GetPets(cancellationToken: TestContext.Current!.Execution.CancellationToken);
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
