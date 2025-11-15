using System.Text.Json;

using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class Mounts(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Mount> actual, MessageContext context) = await sut.Hero.Equipment.Mounts.GetMounts(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id.IsDefined());
            Assert.NotEmpty(entry.Name);
            Assert.True(entry.DefaultSkinId > 0);
            Assert.NotNull(entry.SkinIds);
            Assert.All(entry.SkinIds, id => Assert.True(id > 0));
            Assert.NotNull(entry.Skills);
            Assert.All(entry.Skills, skill =>
            {
                Assert.True(skill.Id > 0);
                Assert.True(skill.Slot.IsDefined());
            });
#if NET
            string json = JsonSerializer.Serialize(entry, GuildWars2JsonContext.Default.Mount);
            Mount? roundtrip = JsonSerializer.Deserialize(json, GuildWars2JsonContext.Default.Mount);
#else
            string json = JsonSerializer.Serialize(entry);
            Mount? roundtrip = JsonSerializer.Deserialize<Mount>(json);
#endif
            Assert.Equal(entry, roundtrip);
        });
    }
}
