﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skins;

[PublicAPI]
public static class WeaponSkinJson
{
    public static WeaponSkin GetWeaponSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("details").GetProperty("type").GetString())
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

        RequiredMember name = new("name");
        OptionalMember description = new("description");
        RequiredMember rarity = new("rarity");
        RequiredMember flags = new("flags");
        RequiredMember restrictions = new("restrictions");
        RequiredMember id = new("id");
        OptionalMember icon = new("icon");
        RequiredMember damageType = new("damage_type");
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
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(rarity.Name))
            {
                rarity.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(restrictions.Name))
            {
                restrictions.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
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
                    else if (detail.NameEquals(damageType.Name))
                    {
                        damageType.Value = detail.Value;
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
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Rarity = rarity.Select(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<SkinFlag>(missingMemberBehavior)),
            Restrictions = restrictions.SelectMany(value => value.GetEnum<SkinRestriction>(missingMemberBehavior)),
            Icon = icon.Select(value => value.GetString()),
            DamageType = damageType.Select(value => value.GetEnum<DamageType>(missingMemberBehavior))
        };
    }
}
