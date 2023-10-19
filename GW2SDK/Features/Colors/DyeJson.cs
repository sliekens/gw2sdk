using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Colors;

[PublicAPI]
public static class DyeJson
{
    public static Dye GetDye(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember baseRgb = new("base_rgb");
        RequiredMember cloth = new("cloth");
        RequiredMember leather = new("leather");
        RequiredMember metal = new("metal");
        OptionalMember fur = new("fur");
        NullableMember itemId = new("item");
        RequiredMember categories = new("categories");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(baseRgb.Name))
            {
                baseRgb = member;
            }
            else if (member.NameEquals(cloth.Name))
            {
                cloth = member;
            }
            else if (member.NameEquals(leather.Name))
            {
                leather = member;
            }
            else if (member.NameEquals(metal.Name))
            {
                metal = member;
            }
            else if (member.NameEquals(fur.Name))
            {
                fur = member;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId = member;
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Dye
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            BaseRgb = baseRgb.Select(value => value.GetColor(missingMemberBehavior)),
            Cloth = cloth.Select(value => value.GetColorInfo(missingMemberBehavior)),
            Leather = leather.Select(value => value.GetColorInfo(missingMemberBehavior)),
            Metal = metal.Select(value => value.GetColorInfo(missingMemberBehavior)),
            Fur = fur.Select(value => value.GetColorInfo(missingMemberBehavior)),
            Item = itemId.Select(value => value.GetInt32()),
            Categories = categories.SelectMany(value => value.GetEnum<ColorCategoryName>(missingMemberBehavior))
        };
    }
}
