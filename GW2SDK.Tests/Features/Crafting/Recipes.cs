using System.Linq;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Crafting;

public class Recipes
{
    [Fact(
        Skip =
            "This test is best used interactively, otherwise it will hit rate limits in this as well as other tests."
    )]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Crafting.GetRecipes().ToListAsync();

        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_output_item_id();
                entry.Has_item_count();
                entry.Has_min_rating_between_0_and_500();
                entry.Has_time_to_craft();
            }
        );
    }
}
