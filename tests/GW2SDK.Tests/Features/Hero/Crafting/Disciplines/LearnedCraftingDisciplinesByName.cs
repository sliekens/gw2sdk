using System.Text.Json;

using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Crafting.Disciplines;

[ServiceDataSource]
public class LearnedCraftingDisciplinesByName(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (LearnedCraftingDisciplines actual, MessageContext context) = await sut.Hero.Crafting.Disciplines.GetLearnedCraftingDisciplines(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.All(actual.Disciplines, entry =>
        {
            Assert.True(entry.Discipline.IsDefined());
            Assert.True(entry.Rating > 0);
        });
        string json = JsonSerializer.Serialize(actual);
        LearnedCraftingDisciplines? roundtrip = JsonSerializer.Deserialize<LearnedCraftingDisciplines>(json);
        Assert.Equal(actual, roundtrip);
    }
}
