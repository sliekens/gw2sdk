using System;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Mounts;

public class MountNames
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountNames();

        Assert.All(
            actual.Value,
            name => Assert.True(Enum.IsDefined(typeof(MountName), name), "Enum.IsDefined(name)")
        );
    }
}
