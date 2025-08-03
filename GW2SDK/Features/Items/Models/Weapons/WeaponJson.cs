using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class WeaponJson
{
    public static Weapon GetWeapon(this in JsonElement json)
    {
        if (json.TryGetProperty("details", out JsonElement discriminator) && discriminator.TryGetProperty("type", out JsonElement subtype))
        {
            switch (subtype.GetString())
            {
                case "Axe":
                    return json.GetAxe();
                case "Dagger":
                    return json.GetDagger();
                case "Focus":
                    return json.GetFocus();
                case "Greatsword":
                    return json.GetGreatsword();
                case "Hammer":
                    return json.GetHammer();
                case "Harpoon":
                    return json.GetSpear();
                case "LargeBundle":
                    return json.GetLargeBundle();
                case "LongBow":
                    return json.GetLongbow();
                case "Mace":
                    return json.GetMace();
                case "Pistol":
                    return json.GetPistol();
                case "Rifle":
                    return json.GetRifle();
                case "Scepter":
                    return json.GetScepter();
                case "Shield":
                    return json.GetShield();
                case "ShortBow":
                    return json.GetShortbow();
                case "SmallBundle":
                    return json.GetSmallBundle();
                case "Speargun":
                    return json.GetHarpoonGun();
                case "Staff":
                    return json.GetStaff();
                case "Sword":
                    return json.GetSword();
                case "Torch":
                    return json.GetTorch();
                case "Toy":
                    return json.GetToy();
                case "ToyTwoHanded":
                    return json.GetToyTwoHanded();
                case "Trident":
                    return json.GetTrident();
                case "Warhorn":
                    return json.GetWarhorn();
            }
        }

        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember level = "level";
        RequiredMember rarity = "rarity";
        RequiredMember vendorValue = "vendor_value";
        RequiredMember defaultSkin = "default_skin";
        RequiredMember gameTypes = "game_types";
        RequiredMember flags = "flags";
        RequiredMember restrictions = "restrictions";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        OptionalMember icon = "icon";
        RequiredMember damageType = "damage_type";
        RequiredMember minPower = "min_power";
        RequiredMember maxPower = "max_power";
        RequiredMember defense = "defense";
        RequiredMember infusionSlots = "infusion_slots";
        RequiredMember attributeAdjustment = "attribute_adjustment";
        OptionalMember statChoices = "stat_choices";
        NullableMember infixUpgradeId = "id";
        OptionalMember infixUpgradeAttributes = "attributes";
        OptionalMember infixUpgradeBuff = "buff";
        NullableMember suffixItemId = "suffix_item_id";
        NullableMember secondarySuffixItemId = "secondary_suffix_item_id";
        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (level.Match(member))
            {
                level = member;
            }
            else if (rarity.Match(member))
            {
                rarity = member;
            }
            else if (vendorValue.Match(member))
            {
                vendorValue = member;
            }
            else if (defaultSkin.Match(member))
            {
                defaultSkin = member;
            }
            else if (gameTypes.Match(member))
            {
                gameTypes = member;
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
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (member.NameEquals("details"))
            {
                foreach (JsonProperty detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            ThrowHelper.ThrowUnexpectedDiscriminator(detail.Value.GetString());
                        }
                    }
                    else if (damageType.Match(detail))
                    {
                        damageType = detail;
                    }
                    else if (minPower.Match(detail))
                    {
                        minPower = detail;
                    }
                    else if (maxPower.Match(detail))
                    {
                        maxPower = detail;
                    }
                    else if (defense.Match(detail))
                    {
                        defense = detail;
                    }
                    else if (infusionSlots.Match(detail))
                    {
                        infusionSlots = detail;
                    }
                    else if (attributeAdjustment.Match(detail))
                    {
                        attributeAdjustment = detail;
                    }
                    else if (statChoices.Match(detail))
                    {
                        statChoices = detail;
                    }
                    else if (detail.NameEquals("infix_upgrade"))
                    {
                        foreach (JsonProperty infix in detail.Value.EnumerateObject())
                        {
                            if (infixUpgradeId.Match(infix))
                            {
                                infixUpgradeId = infix;
                            }
                            else if (infixUpgradeAttributes.Match(infix))
                            {
                                infixUpgradeAttributes = infix;
                            }
                            else if (infixUpgradeBuff.Match(infix))
                            {
                                infixUpgradeBuff = infix;
                            }
                            else if (JsonOptions.MissingMemberBehavior
                                == MissingMemberBehavior.Error)
                            {
                                ThrowHelper.ThrowUnexpectedMember(infix.Name);
                            }
                        }
                    }
                    else if (suffixItemId.Match(detail))
                    {
                        suffixItemId = detail;
                    }
                    else if (secondarySuffixItemId.Match(detail))
                    {
                        secondarySuffixItemId = detail;
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

        string? iconString = icon.Map(static (in JsonElement value) => value.GetString());
        return new Weapon
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Description = description.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Level = level.Map(static (in JsonElement value) => value.GetInt32()),
            Rarity = rarity.Map(static (in JsonElement value) => value.GetEnum<Rarity>()),
            VendorValue = vendorValue.Map(static (in JsonElement value) => value.GetInt32()),
            DefaultSkinId = defaultSkin.Map(static (in JsonElement value) => value.GetInt32()),
            GameTypes =
                gameTypes.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetEnum<GameType>())
                ),
            Flags = flags.Map(static (in JsonElement values) => values.GetItemFlags()),
            Restrictions = restrictions.Map(static (in JsonElement value) => value.GetItemRestriction()),
            ChatLink = chatLink.Map(static (in JsonElement value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null,
            DamageType = damageType.Map(static (in JsonElement value) => value.GetEnum<DamageType>()),
            MinPower = minPower.Map(static (in JsonElement value) => value.GetInt32()),
            MaxPower = maxPower.Map(static (in JsonElement value) => value.GetInt32()),
            Defense = defense.Map(static (in JsonElement value) => value.GetInt32()),
            InfusionSlots =
                infusionSlots.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetInfusionSlot())
                ),
            AttributeAdjustment = attributeAdjustment.Map(static (in JsonElement value) => value.GetDouble()),
            StatChoices =
                statChoices.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32()))
                ?? [],
            AttributeCombinationId = infixUpgradeId.Map(static (in JsonElement value) => value.GetInt32()),
            Attributes = infixUpgradeAttributes.Map(static (in JsonElement values) => values.GetAttributes()) ?? [],
            Buff = infixUpgradeBuff.Map(static (in JsonElement value) => value.GetBuff()),
            SuffixItemId = suffixItemId.Map(static (in JsonElement value) => value.GetInt32()),
            SecondarySuffixItemId = secondarySuffixItemId.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
