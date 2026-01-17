using GuildWars2.Logos;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Logos;

[ServiceDataSource]
public class LogosByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["Guild-Wars-2-logo-en", "Guild-Wars-2-logo-es", "Guild-Wars-2-logo-de",];
        (IImmutableValueSet<Logo> actual, MessageContext context) = await sut.Logos.GetLogosByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).Count().IsEqualTo(ids.Count);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, c => c.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(ids.Count);
            foreach (string id in ids)
            {
                await Assert.That(actual).Contains(found => found.Id == id);
            }
        }
    }
}
