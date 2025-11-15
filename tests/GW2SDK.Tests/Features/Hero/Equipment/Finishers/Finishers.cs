using System.Text.Json;

using GuildWars2.Hero.Equipment.Finishers;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Finishers;

[ServiceDataSource]
public class Finishers(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Finisher> actual, MessageContext context) = await sut.Hero.Equipment.Finishers.GetFinishers(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotNull(entry.LockedText);
            MarkupSyntaxValidator.Validate(entry.LockedText);
            Assert.NotNull(entry.UnlockItemIds);
            Assert.All(entry.UnlockItemIds, id => Assert.True(id > 0));
            Assert.True(entry.Order >= 0);
            Assert.NotNull(entry.IconUrl);
            Assert.True(entry.IconUrl.IsAbsoluteUri || entry.IconUrl.IsWellFormedOriginalString());
            Assert.NotEmpty(entry.Name);
            string json;
            Finisher? roundTrip;
#if NET
            json = JsonSerializer.Serialize(entry, GuildWars2JsonContext.Default.Finisher);
            roundTrip = JsonSerializer.Deserialize(json, GuildWars2JsonContext.Default.Finisher);
#else
            json = JsonSerializer.Serialize(entry);
            roundTrip = JsonSerializer.Deserialize<Finisher>(json);
#endif
            Assert.Equal(entry, roundTrip);
        });
    }
}
