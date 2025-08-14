using System.Text.Json;

using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Disciplines;

public class LearnedCraftingDisciplinesByName
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;

        (LearnedCraftingDisciplines actual, MessageContext context) = await sut.Hero.Crafting.Disciplines.GetLearnedCraftingDisciplines(
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

        string json = JsonSerializer.Serialize(actual);
        LearnedCraftingDisciplines? roundtrip = JsonSerializer.Deserialize<LearnedCraftingDisciplines>(json);
        Assert.Equal(actual, roundtrip);
    }
}
