using System.Text.Json;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Disciplines;

public class LearnedCraftingDisciplinesByName
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = TestConfiguration.TestCharacter;
        var accessToken = TestConfiguration.ApiKey;

        var (actual, context) = await sut.Hero.Crafting.Disciplines.GetLearnedCraftingDisciplines(
            character.Name,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.All(
            actual.Disciplines,
            entry =>
            {
                Assert.True(entry.Discipline.IsDefined());
                Assert.True(entry.Rating > 0);
            }
        );

        var json = JsonSerializer.Serialize(actual);
        var roundtrip = JsonSerializer.Deserialize<LearnedCraftingDisciplines>(json);
        Assert.Equal(actual, roundtrip);
    }
}
