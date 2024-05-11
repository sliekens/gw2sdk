using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Dyes;

internal static class DyeColorJson
{
    public static DyeColor GetDyeColor(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember baseRgb = "base_rgb";
        RequiredMember cloth = "cloth";
        RequiredMember leather = "leather";
        RequiredMember metal = "metal";
        OptionalMember fur = "fur";
        NullableMember itemId = "item";
        RequiredMember categories = "categories";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (baseRgb.Match(member))
            {
                baseRgb = member;
            }
            else if (cloth.Match(member))
            {
                cloth = member;
            }
            else if (leather.Match(member))
            {
                leather = member;
            }
            else if (metal.Match(member))
            {
                metal = member;
            }
            else if (fur.Match(member))
            {
                fur = member;
            }
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (categories.Match(member))
            {
                categories = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        // The API puts all hues, materials, and color sets into the same array,
        // which is uncomfortable, so split them into properties
        var (hue, material, set) = categories.Map(static value => value.GetCategories());

        // the first element is the hue, second is material, third is color set
        return new DyeColor
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            BaseRgb = baseRgb.Map(static value => value.GetColor()),
            Cloth = cloth.Map(static value => value.GetColorInfo()),
            Leather = leather.Map(static value => value.GetColorInfo()),
            Metal = metal.Map(static value => value.GetColorInfo()),
            Fur = fur.Map(static value => value.GetColorInfo()),
            ItemId = itemId.Map(static value => value.GetInt32()),
            Hue = hue,
            Material = material,
            Set = set
        };
    }
}
