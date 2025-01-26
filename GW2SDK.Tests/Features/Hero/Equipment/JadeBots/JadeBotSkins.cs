using System.Text.Json;
using GuildWars2.Hero.Equipment.JadeBots;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.JadeBots;

public class JadeBotSkins
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Hero.Equipment.JadeBots.GetJadeBotSkins(
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
                Assert.NotEmpty(entry.Name);

                // Missing description for Roundtail Dragon
                if (entry.Id == 6)
                {
                    Assert.Empty(entry.Description);
                }
                else
                {
                    Assert.NotEmpty(entry.Description);
                    MarkupSyntaxValidator.Validate(entry.Description);
                }

                Assert.True(entry.UnlockItemId > 0);

                var json = JsonSerializer.Serialize(entry);
                var roundtrip = JsonSerializer.Deserialize<JadeBotSkin>(json);
                Assert.Equal(entry, roundtrip);
            }
        );
    }
}
