using GuildWars2.Files;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Files;

[ServiceDataSource]
public class FilesByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["map_complete", "map_vendor_ecto", "map_stairs_up"];
        (IImmutableValueSet<Asset> actual, MessageContext context) = await sut.Files.GetFilesByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsNotNull().And.IsGreaterThan(ids.Count));
        await Assert.That(actual).Count().IsEqualTo(ids.Count);
        using (Assert.Multiple())
        {
            foreach (string id in ids)
            {
                await Assert.That(actual).Contains(found => found.Id == id);
            }
        }
    }
}
