using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WorldBosses;

public class DefeatedWorldBosses
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        // Resets each day, not easy to prove it works
        var (actual, _) = await sut.WorldBosses.GetDefeatedWorldBosses(accessToken.Key);

        HashSet<string> expected = new()
        {
            "admiral_taidha_covington",
            "claw_of_jormag",
            "drakkar",
            "fire_elemental",
            "great_jungle_wurm",
            "inquest_golem_mark_ii",
            "karka_queen",
            "megadestroyer",
            "modniir_ulgoth",
            "shadow_behemoth",
            "svanir_shaman_chief",
            "tequatl_the_sunless",
            "the_shatterer",
            "triple_trouble_wurm"
        };

        // The best we can do is verify that there are no unexpected bosses
        Assert.Subset(expected, actual);
    }
}
