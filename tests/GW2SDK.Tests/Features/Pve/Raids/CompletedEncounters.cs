using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Raids;

public class CompletedEncounters
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<string> actual, _) = await sut.Pve.Raids.GetCompletedEncounters(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        // Can be empty if you haven't done any raids this week
        // The best we can do is verify that there are no unexpected encounters
        Assert.All(actual, encounter => Assert.Contains(encounter, new[] { // W1-W3 "forsaken_thicket"
 "vale_guardian", "spirit_woods", "gorseval", "sabetha", "slothasor", "bandit_trio", "matthias", "escort", "keep_construct", "twisted_castle", "xera", // W4 "bastion_of_the_penitent"
 "cairn", "mursaat_overseer", "samarog", "deimos", // W5 "hall_of_chains"
 "soulless_horror", "river_of_souls", "statues_of_grenth", "voice_in_the_void", // W6 "mythwright_gambit"
 "conjured_amalgamate", "twin_largos", "qadim", // W7 "the_key_of_ahdashim"
 "gate", "adina", "sabir", "qadim_the_peerless", // W8 "mount_balrior"
 "camp", "greer", "decima", "ura" }));
    }
}
