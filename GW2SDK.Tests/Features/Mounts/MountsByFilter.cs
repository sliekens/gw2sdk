using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Mounts;

public class MountsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<MountName> names = new()
        {
            MountName.Raptor,
            MountName.Jackal,
            MountName.Skimmer
        };

        var actual = await sut.Mounts.GetMountsByNames(names);

        Assert.Collection(
            names,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

}
