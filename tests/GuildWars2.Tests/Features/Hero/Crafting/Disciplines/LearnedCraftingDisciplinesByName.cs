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
        await Assert.That(context).IsNotNull();
        foreach (CraftingDiscipline entry in actual.Disciplines)
        {
            await Assert.That(entry.Discipline.IsDefined()).IsTrue();
            await Assert.That(entry.Rating).IsGreaterThan(0);
        }

        string json;
        LearnedCraftingDisciplines? roundtrip;
#if NET
        json = JsonSerializer.Serialize(actual, Common.TestJsonContext.Default.LearnedCraftingDisciplines);
        roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.LearnedCraftingDisciplines);
#else
        json = JsonSerializer.Serialize(actual);
        roundtrip = JsonSerializer.Deserialize<LearnedCraftingDisciplines>(json);
#endif
        await Assert.That(roundtrip).IsEqualTo(actual);
    }
}
