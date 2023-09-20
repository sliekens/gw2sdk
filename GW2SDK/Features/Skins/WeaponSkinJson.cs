using System.Text.Json;
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

        RequiredMember<string> name = new("name");
        OptionalMember<string> description = new("description");
        RequiredMember<Rarity> rarity = new("rarity");
        RequiredMember<SkinFlag> flags = new("flags");
        RequiredMember<SkinRestriction> restrictions = new("restrictions");
        RequiredMember<int> id = new("id");
        OptionalMember<string> icon = new("icon");
        RequiredMember<DamageType> damageType = new("damage_type");
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
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValueOrEmpty(),
            Rarity = rarity.GetValue(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Restrictions = restrictions.GetValues(missingMemberBehavior),
            Icon = icon.GetValueOrNull(),
            DamageType = damageType.GetValue(missingMemberBehavior)
        };
    }
}
