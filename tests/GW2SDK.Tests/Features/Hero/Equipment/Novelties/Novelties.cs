using System.Text.Json;

using GuildWars2.Hero.Equipment.Novelties;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Novelties;

[ServiceDataSource]
public class Novelties(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Novelty> actual, MessageContext context) = await sut.Hero.Equipment.Novelties.GetNovelties(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.NotNull(entry.Description);
            MarkupSyntaxValidator.Validate(entry.Description);
            Assert.True(entry.IconUrl.IsAbsoluteUri);
            Assert.True(entry.Slot.IsDefined());
            Assert.NotEmpty(entry.UnlockItemIds);
#if NET
            string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.Novelty);
            Novelty? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Novelty);
#else
            string json = JsonSerializer.Serialize(entry);
            Novelty? roundtrip = JsonSerializer.Deserialize<Novelty>(json);
#endif
            Assert.Equal(entry, roundtrip);
        });
    }
}
