using System;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Professions;

public class Professions
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Professions.GetProfessions();

        Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Value.Count);

        Assert.All(
            actual.Value,
            profession =>
            {
                Assert.True(
                    Enum.IsDefined(typeof(ProfessionName), profession.Id),
                    "Enum.IsDefined(profession.Id)"
                );
                Assert.NotEmpty(profession.Name);
                Assert.NotEmpty(profession.Icon);
                Assert.NotEmpty(profession.IconBig);
            }
        );
    }
}
