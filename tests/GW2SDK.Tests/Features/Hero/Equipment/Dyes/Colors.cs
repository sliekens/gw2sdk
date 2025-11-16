using System.Text.Json;

using GuildWars2.Chat;
using GuildWars2.Hero.Equipment.Dyes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Dyes;

[ServiceDataSource]
public class Colors(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<DyeColor> actual, MessageContext context) = await sut.Hero.Equipment.Dyes.GetColors(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);

        using (Assert.Multiple())
        {
            foreach (DyeColor color in actual)
            {
                await Assert.That(color.Id).IsGreaterThan(0);
                await Assert.That(color.Name).IsNotEmpty();
                await Assert.That(color.BaseRgb.IsEmpty).IsFalse();
                await Assert.That(color.Cloth.Rgb.IsEmpty).IsFalse();
                await Assert.That(color.Leather.Rgb.IsEmpty).IsFalse();
                await Assert.That(color.Metal.Rgb.IsEmpty).IsFalse();

                if (color.Fur is not null)
                {
                    await Assert.That(color.Fur.Rgb.IsEmpty).IsFalse();
                }

                await Assert.That(color.Hue.IsDefined()).IsTrue();
                await Assert.That(color.Material.IsDefined()).IsTrue();
                await Assert.That(color.Set.IsDefined()).IsTrue();

                if (color.ItemId.HasValue)
                {
                    ItemLink? link = color.GetChatLink();
                    await Assert.That(link?.ItemId).IsEqualTo(color.ItemId);
                }

                string json;
                DyeColor? roundTrip;
#if NET
                json = JsonSerializer.Serialize(color, Common.TestJsonContext.Default.DyeColor);
                roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.DyeColor);
#else
                json = JsonSerializer.Serialize(color);
                roundTrip = JsonSerializer.Deserialize<DyeColor>(json);
#endif
                await Assert.That(roundTrip).IsTypeOf<DyeColor>();
                await Assert.That(color).IsEqualTo(roundTrip);
            }
        }
    }
}
