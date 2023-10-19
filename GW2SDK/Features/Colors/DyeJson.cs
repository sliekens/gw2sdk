﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Colors;

[PublicAPI]
public static class DyeJson
{
    public static Dye GetDye(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            BaseRgb = baseRgb.Map(value => value.GetColor(missingMemberBehavior)),
            Cloth = cloth.Map(value => value.GetColorInfo(missingMemberBehavior)),
            Leather = leather.Map(value => value.GetColorInfo(missingMemberBehavior)),
            Metal = metal.Map(value => value.GetColorInfo(missingMemberBehavior)),
            Fur = fur.Map(value => value.GetColorInfo(missingMemberBehavior)),
            Item = itemId.Map(value => value.GetInt32()),
            Categories = categories.Map(values => values.GetList(value => value.GetEnum<ColorCategoryName>(missingMemberBehavior)))
        };
    }
}
