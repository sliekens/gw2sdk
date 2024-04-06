using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<MountName> names =
        [
            MountName.Raptor, MountName.Jackal,
            MountName.Skimmer
        ];

        var (actual, context) = await sut.Hero.Equipment.Mounts.GetMountsByNames(names);

        Assert.Equal(names.Count, context.ResultCount);
        Assert.True(context.ResultTotal > names.Count);
        Assert.Equal(names.Count, actual.Count);
        Assert.Collection(
            names,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}
