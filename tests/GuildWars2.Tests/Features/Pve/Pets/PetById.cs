using GuildWars2.Pve.Pets;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Pets;

[ServiceDataSource]
public class PetById(Gw2Client sut)
{
    [Retry(3, RetryOnExceptionTypes = new[] { typeof(System.Net.Http.HttpRequestException) })]
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1;
        (Pet actual, MessageContext context) = await sut.Pve.Pets.GetPetById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
