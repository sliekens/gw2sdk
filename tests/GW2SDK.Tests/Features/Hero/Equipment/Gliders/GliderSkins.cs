using System.Text.Json;

using GuildWars2.Hero.Equipment.Gliders;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

[ServiceDataSource]
public class GliderSkins(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<GliderSkin> actual, MessageContext context) = await sut.Hero.Equipment.Gliders.GetGliderSkins(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotNull(entry.UnlockItemIds);
            Assert.True(entry.Order >= 0);
            Assert.True(entry.IconUrl.IsAbsoluteUri);
            Assert.NotEmpty(entry.Name);
            Assert.NotNull(entry.Description);
            MarkupSyntaxValidator.Validate(entry.Description);
            Assert.NotNull(entry.DefaultDyeColorIds);
#if NET
            string json = JsonSerializer.Serialize(entry, GuildWars2JsonContext.Default.GliderSkin);
            GliderSkin? roundTrip = JsonSerializer.Deserialize(json, GuildWars2JsonContext.Default.GliderSkin);
#else
            string json = JsonSerializer.Serialize(entry);
            GliderSkin? roundTrip = JsonSerializer.Deserialize<GliderSkin>(json);
#endif
            Assert.Equal(entry, roundTrip);
        });
    }
}
