using System.Text.Json;

using GuildWars2.Hero.Equipment.Novelties;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Novelties;

public class Novelties
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Novelty> actual, MessageContext context) = await sut.Hero.Equipment.Novelties.GetNovelties(cancellationToken: TestContext.Current!.CancellationToken);
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
            string json = JsonSerializer.Serialize(entry);
            Novelty? roundtrip = JsonSerializer.Deserialize<Novelty>(json);
            Assert.Equal(entry, roundtrip);
        });
    }
}
