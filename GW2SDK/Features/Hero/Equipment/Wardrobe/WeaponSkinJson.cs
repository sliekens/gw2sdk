﻿using System.Text.Json;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class WeaponSkinJson
{
    public static WeaponSkin GetWeaponSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        if (json.TryGetProperty("details", out var discriminator))
        {
            if (discriminator.TryGetProperty("type", out var subtype))
            {
                switch (subtype.GetString())
                {
                    case "Axe":
                        return json.GetAxeSkin(missingMemberBehavior);
                    case "Dagger":
                        return json.GetDaggerSkin(missingMemberBehavior);
                    case "Focus":
                        return json.GetFocusSkin(missingMemberBehavior);
                    case "Greatsword":
                        return json.GetGreatswordSkin(missingMemberBehavior);
                    case "Hammer":
                        return json.GetHammerSkin(missingMemberBehavior);
                    case "LargeBundle":
                        return json.GetLargeBundleSkin(missingMemberBehavior);
                    case "Longbow":
                        return json.GetLongbowSkin(missingMemberBehavior);
                    case "Mace":
                        return json.GetMaceSkin(missingMemberBehavior);
                    case "Pistol":
                        return json.GetPistolSkin(missingMemberBehavior);
                    case "Rifle":
                        return json.GetRifleSkin(missingMemberBehavior);
                    case "Scepter":
                        return json.GetScepterSkin(missingMemberBehavior);
                    case "Shield":
                        return json.GetShieldSkin(missingMemberBehavior);
                    case "Shortbow":
                        return json.GetShortbowSkin(missingMemberBehavior);
                    case "SmallBundle":
                        return json.GetSmallBundleSkin(missingMemberBehavior);
                    case "Spear":
                        return json.GetSpearSkin(missingMemberBehavior);
                    case "Speargun":
                        return json.GetHarpoonGunSkin(missingMemberBehavior);
                    case "Staff":
                        return json.GetStaffSkin(missingMemberBehavior);
                    case "Sword":
                        return json.GetSwordSkin(missingMemberBehavior);
                    case "Torch":
                        return json.GetTorchSkin(missingMemberBehavior);
                    case "Toy":
                        return json.GetToySkin(missingMemberBehavior);
                    case "ToyTwoHanded":
                        return json.GetToyTwoHandedSkin(missingMemberBehavior);
                    case "Trident":
                        return json.GetTridentSkin(missingMemberBehavior);
                    case "Warhorn":
                        return json.GetWarhornSkin(missingMemberBehavior);
                }
            }
        }

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
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
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
            else if (member.NameEquals("details"))
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException(
                                Strings.UnexpectedDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (damageType.Match(detail))
                    {
                        damageType = detail;
                    }
                    else if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedMember(detail.Name));
                    }
                }
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponSkin
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            Rarity = rarity.Map(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            Flags = flags.Map(values => values.GetSkinFlags()),
            Races = restrictions.Map(values => values.GetRestrictions(missingMemberBehavior)),
            IconHref = icon.Map(value => value.GetString()),
            DamageType = damageType.Map(value => value.GetEnum<DamageType>(missingMemberBehavior))
        };
    }
}
