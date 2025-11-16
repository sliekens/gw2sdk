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
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        using (Assert.Multiple())
        {
            foreach (Mount entry in actual)
            {
                await Assert.That(entry.Id.IsDefined()).IsTrue();
                await Assert.That(entry.Name).IsNotEmpty();
                await Assert.That(entry.DefaultSkinId).IsGreaterThan(0);
                await Assert.That(entry.SkinIds).IsNotNull();
                using (Assert.Multiple())
                {
                    foreach (int id in entry.SkinIds)
                    {
                        await Assert.That(id).IsGreaterThan(0);
                    }
                }
                await Assert.That(entry.Skills).IsNotNull();
                using (Assert.Multiple())
                {
                    foreach (SkillReference skill in entry.Skills)
                    {
                        await Assert.That(skill.Id).IsGreaterThan(0);
                        await Assert.That(skill.Slot.IsDefined()).IsTrue();
                    }
                }
#if NET
                string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.Mount);
                Mount? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Mount);
#else
                string json = JsonSerializer.Serialize(entry);
                Mount? roundtrip = JsonSerializer.Deserialize<Mount>(json);
#endif
                await Assert.That(roundtrip).IsEqualTo(entry);
            }
        }
    }
}
