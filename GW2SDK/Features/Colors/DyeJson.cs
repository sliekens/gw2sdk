using System.Drawing;
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Colors;

[PublicAPI]
public static class DyeJson
{
    public static Dye GetDye(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<Color> baseRgb = new("base_rgb");
        RequiredMember<ColorInfo> cloth = new("cloth");
        RequiredMember<ColorInfo> leather = new("leather");
        RequiredMember<ColorInfo> metal = new("metal");
        OptionalMember<ColorInfo> fur = new("fur");
        NullableMember<int> itemId = new("item");
        RequiredMember<ColorCategoryName> categories = new("categories");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(baseRgb.Name))
            {
                baseRgb.Value = member.Value;
            }
            else if (member.NameEquals(cloth.Name))
            {
                cloth.Value = member.Value;
            }
            else if (member.NameEquals(leather.Name))
            {
                leather.Value = member.Value;
            }
            else if (member.NameEquals(metal.Name))
            {
                metal.Value = member.Value;
            }
            else if (member.NameEquals(fur.Name))
            {
                fur.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (member.NameEquals(categories.Name))
            {
                categories.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Dye
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            BaseRgb = baseRgb.Select(value => value.GetColor()),
            Cloth = cloth.Select(value => value.GetColorInfo(missingMemberBehavior)),
            Leather = leather.Select(value => value.GetColorInfo(missingMemberBehavior)),
            Metal = metal.Select(value => value.GetColorInfo(missingMemberBehavior)),
            Fur = fur.Select(value => value.GetColorInfo(missingMemberBehavior)),
            Item = itemId.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }
}
