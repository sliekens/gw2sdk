using System.Text.Json;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class WeaponSkinJson
{
    public static WeaponSkin GetWeaponSkin(this JsonElement json)
    {
        if (json.TryGetProperty("details", out var discriminator))
        {
            if (discriminator.TryGetProperty("type", out var subtype))
            {
                switch (subtype.GetString())
                {
                    case "Axe":
                        return json.GetAxeSkin();
                    case "Dagger":
                        return json.GetDaggerSkin();
                    case "Focus":
                        return json.GetFocusSkin();
                    case "Greatsword":
                        return json.GetGreatswordSkin();
                    case "Hammer":
                        return json.GetHammerSkin();
                    case "LargeBundle":
                        return json.GetLargeBundleSkin();
                    case "Longbow":
                        return json.GetLongbowSkin();
                    case "Mace":
                        return json.GetMaceSkin();
                    case "Pistol":
                        return json.GetPistolSkin();
                    case "Rifle":
                        return json.GetRifleSkin();
                    case "Scepter":
                        return json.GetScepterSkin();
                    case "Shield":
                        return json.GetShieldSkin();
                    case "Shortbow":
                        return json.GetShortbowSkin();
                    case "SmallBundle":
                        return json.GetSmallBundleSkin();
                    case "Spear":
                        return json.GetSpearSkin();
                    case "Speargun":
                        return json.GetHarpoonGunSkin();
                    case "Staff":
                        return json.GetStaffSkin();
                    case "Sword":
                        return json.GetSwordSkin();
                    case "Torch":
                        return json.GetTorchSkin();
                    case "Toy":
                        return json.GetToySkin();
                    case "ToyTwoHanded":
                        return json.GetToyTwoHandedSkin();
                    case "Trident":
                        return json.GetTridentSkin();
                    case "Warhorn":
                        return json.GetWarhornSkin();
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
                        if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
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
                    else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedMember(detail.Name));
                    }
                }
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponSkin
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
