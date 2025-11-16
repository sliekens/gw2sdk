using GuildWars2.Hero;
using GuildWars2.Hero.Equipment.Templates;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

internal sealed class EquipmentItemValidation
{
    public static async Task Validate(EquipmentItem item)
    {
        await Assert.That(item.Id).IsGreaterThan(0);
        if (item.Count.HasValue)
        {
            await Assert.That(item.Count.Value).IsGreaterThan(0);
        }

        if (item.Slot.HasValue)
        {
            await Assert.That(item.Slot.Value.IsDefined()).IsTrue();
        }

        if (item.SuffixItemId.HasValue)
        {
            await Assert.That(item.SuffixItemId.Value).IsGreaterThan(0);
        }

        if (item.SecondarySuffixItemId.HasValue)
        {
            await Assert.That(item.SecondarySuffixItemId.Value).IsGreaterThan(0);
        }

        await Assert.That(item.InfusionItemIds).IsNotNull();
        using (Assert.Multiple())
        {
            foreach (int infusionItemId in item.InfusionItemIds)
            {
                await Assert.That(infusionItemId).IsGreaterThan(0);
            }
        }

        if (item.SkinId.HasValue)
        {
            await Assert.That(item.SkinId.Value).IsGreaterThan(0);
        }

        if (item.Stats is not null)
        {
            await Assert.That(item.Stats.Id).IsGreaterThan(0);
            await Assert.That(item.Stats.Attributes).IsNotEmpty();
            using (Assert.Multiple())
            {
                foreach (KeyValuePair<AttributeName, int> attribute in item.Stats.Attributes)
                {
#if NET
                    await Assert.That(Enum.IsDefined(attribute.Key)).IsTrue();
#else
                    await TUnit.Assertions.Assert.That(Enum.IsDefined(typeof(AttributeName), attribute.Key)).IsTrue();
#endif
                    await Assert.That(attribute.Value).IsGreaterThan(0);
                }
            }
        }

        await Assert.That(item.Binding.IsDefined()).IsTrue();
        if (item.Binding == ItemBinding.Character)
        {
            await Assert.That(item.BoundTo).IsNotEmpty();
        }
        else
        {
            await Assert.That(item.BoundTo).IsNotNull();
        }

        await Assert.That(item.Location.IsDefined()).IsTrue();

        await Assert.That(item.TemplateNumbers).IsNotNull();
        using (Assert.Multiple())
        {
            foreach (int template in item.TemplateNumbers)
            {
                await Assert.That(template).IsGreaterThan(0);
            }
        }

        await Assert.That(item.DyeColorIds).IsNotNull();
        using (Assert.Multiple())
        {
            foreach (int dyeColorId in item.DyeColorIds)
            {
                await Assert.That(dyeColorId).IsGreaterThan(0);
            }
        }
    }
}
