using System.Text.Json;

using GuildWars2.Hero.Equipment.Skiffs;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

[ServiceDataSource]
public class SkiffSkins(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<SkiffSkin> actual, MessageContext context) = await sut.Hero.Equipment.Skiffs.GetSkiffSkins(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.NotNull(entry.IconUrl);
            Assert.True(entry.IconUrl == null || entry.IconUrl.IsAbsoluteUri || entry.IconUrl.IsWellFormedOriginalString());
            Assert.NotNull(entry.DyeSlots);
            Assert.All(entry.DyeSlots, dyeSlot =>
            {
                Assert.True(dyeSlot.Material.IsDefined());
                Assert.True(dyeSlot.ColorId > 0);
            });
            string json = JsonSerializer.Serialize(entry);
            SkiffSkin? roundtrip = JsonSerializer.Deserialize<SkiffSkin>(json);
            Assert.Equal(entry, roundtrip);
        });
    }
}
