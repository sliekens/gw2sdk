using System.Threading.Tasks;
using GW2SDK.Characters;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Characters
{
    public class CharacterServiceTest
    {
        [Fact]
        public async Task It_can_get_all_character_names()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CharacterService>();
            var accessToken = services.Resolve<ApiKeyFull>();

            var actual = await sut.GetCharactersIndex(accessToken.Key);

            var expected = services.Resolve<TestCharacterName>()
                .Name;

            Assert.Contains(expected, actual);
        }

        [Fact]
        public async Task It_can_get_all_characters()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CharacterService>();
            var accessToken = services.Resolve<ApiKeyFull>();

            var actual = await sut.GetCharacters(accessToken.Key);
            
            Assert.Equal(actual.Context.ResultTotal, actual.Count);
        }

        [Fact]
        public async Task It_can_get_a_character_by_name()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CharacterService>();
            var characterName = services.Resolve<TestCharacterName>();
            var accessToken = services.Resolve<ApiKeyFull>();

            var actual = await sut.GetCharacterByName(characterName.Name, accessToken.Key);

            Assert.Equal(characterName.Name, actual.Value.Name);
        }

        [Fact]
        public async Task It_can_get_the_unlocked_recipes()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CharacterService>();
            var characterName = services.Resolve<TestCharacterName>();
            var accessToken = services.Resolve<ApiKeyFull>();

            var actual = await sut.GetUnlockedRecipes(characterName.Name, accessToken.Key);

            Assert.All(actual.Value.Recipes, id => Assert.NotEqual(0, id));
        }
    }
}
