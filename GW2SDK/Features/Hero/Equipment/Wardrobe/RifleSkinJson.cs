using System.Text.Json;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class RifleSkinJson
{
    public static RifleSkin GetRifleSkin(this JsonElement json)
    {
        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember rarity = "rarity";
        RequiredMember flags = "flags";
        RequiredMember restrictions = "restrictions";
        RequiredMember id = "id";
        OptionalMember icon = "icon";
        RequiredMember damageType = "damage_type";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Weapon"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (rarity.Match(member))
            {
                rarity = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (restrictions.Match(member))
            {
                restrictions = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (member.NameEquals("details"))
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (!detail.Value.ValueEquals("Rifle"))
                        {
                            ThrowHelper.ThrowInvalidDiscriminator(detail.Value.GetString());
                        }
                    }
                    else if (damageType.Match(detail))
                    {
                        damageType = detail;
                    }
                    else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        ThrowHelper.ThrowUnexpectedMember(detail.Name);
                    }
                }
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new RifleSkin
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetString()) ?? "",
            Rarity = rarity.Map(static value => value.GetEnum<Rarity>()),
            Flags = flags.Map(static values => values.GetSkinFlags()),
            Races = restrictions.Map(static values => values.GetRestrictions()),
            IconHref = icon.Map(static value => value.GetString()),
            DamageType = damageType.Map(static value => value.GetEnum<DamageType>())
        };
    }
}
