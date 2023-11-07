using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting;

public class LearnedCraftingDisciplines
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Crafting.GetLearnedCraftingDisciplines(character.Name, accessToken.Key);

        Assert.NotEmpty(actual.Disciplines);
        Assert.All(
            actual.Disciplines,
            entry => Assert.True(Enum.IsDefined(typeof(CraftingDisciplineName), entry.Discipline))
        );
        Assert.All(actual.Disciplines, entry => Assert.True(entry.Rating > 0));
    }
}
