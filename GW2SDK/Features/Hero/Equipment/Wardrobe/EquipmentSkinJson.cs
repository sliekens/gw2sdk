﻿using System.Text.Json;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class EquipmentSkinJson
{
    public static EquipmentSkin GetEquipmentSkin(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Armor":
                    return json.GetArmorSkin(missingMemberBehavior);
                case "Back":
                    return json.GetBackpackSkin(missingMemberBehavior);
                case "Gathering":
                    return json.GetGatheringToolSkin(missingMemberBehavior);
                case "Weapon":
                    return json.GetWeaponSkin(missingMemberBehavior);
            }
        }

        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember rarity = "rarity";
        RequiredMember flags = "flags";
        RequiredMember restrictions = "restrictions";
        RequiredMember id = "id";
        OptionalMember icon = "icon";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EquipmentSkin
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            Rarity = rarity.Map(value => value.GetEnum<Rarity>()),
            Flags = flags.Map(values => values.GetSkinFlags()),
            Races = restrictions.Map(values => values.GetRestrictions()),
            IconHref = icon.Map(value => value.GetString())
        };
    }
}
