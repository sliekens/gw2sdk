using System.Text.Json;
using GuildWars2.Hero.Equipment.Gliders;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

public class GliderSkins
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Gliders.GetGliderSkins(
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotNull(entry.UnlockItemIds);
                Assert.True(entry.Order >= 0);
                Assert.True(entry.IconUrl.IsAbsoluteUri);
                Assert.NotEmpty(entry.Name);
                Assert.NotNull(entry.Description);
                MarkupSyntaxValidator.Validate(entry.Description);
                Assert.NotNull(entry.DefaultDyeColorIds);

                var json = JsonSerializer.Serialize(entry);
                var roundTrip = JsonSerializer.Deserialize<GliderSkin>(json);
                Assert.Equal(entry, roundTrip);
            }
        );
    }
}
