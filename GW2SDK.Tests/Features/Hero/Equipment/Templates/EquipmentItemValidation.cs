using GuildWars2.Hero;
using GuildWars2.Hero.Equipment.Templates;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

internal class EquipmentItemValidation
{
    public static void Validate(EquipmentItem item)
    {
        Assert.True(item.Id > 0);
        if (item.Count.HasValue)
        {
            Assert.True(item.Count > 0);
        }

        if (item.Slot.HasValue)
        {
            Assert.True(item.Slot.Value.IsDefined());
        }

        if (item.SuffixItemId.HasValue)
        {
            Assert.True(item.SuffixItemId > 0);
        }

        if (item.SecondarySuffixItemId.HasValue)
        {
            Assert.True(item.SecondarySuffixItemId > 0);
        }

        Assert.NotNull(item.InfusionItemIds);
        Assert.All(
            item.InfusionItemIds,
            infusionItemId =>
            {
                Assert.True(infusionItemId > 0);
            }
        );

        if (item.SkinId.HasValue)
        {
            Assert.True(item.SkinId > 0);
        }

        if (item.Stats is not null)
        {
            Assert.True(item.Stats.Id > 0);
            Assert.NotEmpty(item.Stats.Attributes);
            Assert.All(
                item.Stats.Attributes,
                attribute =>
                {
#if NET
                    Assert.True(Enum.IsDefined<AttributeName>(attribute.Key));
#else
                    Assert.True(Enum.IsDefined(typeof(AttributeName), attribute.Key));
#endif
                    Assert.True(attribute.Value > 0);
                }
            );
        }

        Assert.True(item.Binding.IsDefined());
        if (item.Binding == ItemBinding.Character)
        {
            Assert.NotEmpty(item.BoundTo);
        }
        else
        {
            Assert.NotNull(item.BoundTo);
        }

        Assert.True(item.Location.IsDefined());

        Assert.NotNull(item.TemplateNumbers);
        Assert.All(
            item.TemplateNumbers,
            template =>
            {
                Assert.True(template > 0);
            }
        );

        Assert.NotNull(item.DyeColorIds);
        Assert.All(
            item.DyeColorIds,
            dyeColorId =>
            {
                Assert.True(dyeColorId > 0);
            }
        );
    }
}
