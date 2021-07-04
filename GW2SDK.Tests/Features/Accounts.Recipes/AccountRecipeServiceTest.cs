﻿using System.Threading.Tasks;
using GW2SDK.Accounts.Recipes;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Recipes
{
    public class AccountRecipeServiceTest
    {
        [Fact]
        public async Task It_can_get_the_unlocked_recipes()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AccountRecipesService>();

            var actual = await sut.GetUnlockedRecipes(ConfigurationManager.Instance.ApiKeyFull);

            Assert.NotEmpty(actual.Value);
            Assert.All(actual.Value, id => Assert.NotEqual(0, id));
        }
    }
}
