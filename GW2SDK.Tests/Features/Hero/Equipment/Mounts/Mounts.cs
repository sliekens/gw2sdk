using System.Text.Json;
using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class Mounts
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Mounts.GetMounts(
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id.IsDefined());
                Assert.NotEmpty(entry.Name);
                Assert.True(entry.DefaultSkinId > 0);
                Assert.NotNull(entry.SkinIds);
                Assert.All(entry.SkinIds, id => Assert.True(id > 0));
                Assert.NotNull(entry.Skills);
                Assert.All(
                    entry.Skills,
                    skill =>
                    {
                        Assert.True(skill.Id > 0);
                        Assert.True(skill.Slot.IsDefined());
                    }
                );

                var json = JsonSerializer.Serialize(entry);
                var roundtrip = JsonSerializer.Deserialize<Mount>(json);
                Assert.Equal(entry, roundtrip);
            }
        );
    }
}
