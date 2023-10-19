using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class ShortbowJson
{
    public static Shortbow GetShortbow(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = new("name");
        OptionalMember description = new("description");
        RequiredMember level = new("level");
        RequiredMember rarity = new("rarity");
        RequiredMember vendorValue = new("vendor_value");
        RequiredMember defaultSkin = new("default_skin");
        RequiredMember gameTypes = new("game_types");
        RequiredMember flags = new("flags");
        RequiredMember restrictions = new("restrictions");
        RequiredMember id = new("id");
        RequiredMember chatLink = new("chat_link");
        OptionalMember icon = new("icon");
        RequiredMember damageType = new("damage_type");
        RequiredMember minPower = new("min_power");
        RequiredMember maxPower = new("max_power");
        RequiredMember defense = new("defense");
        RequiredMember infusionSlots = new("infusion_slots");
        RequiredMember attributeAdjustment = new("attribute_adjustment");
        OptionalMember statChoices = new("stat_choices");
        OptionalMember infixUpgrade = new("infix_upgrade");
        NullableMember suffixItemId = new("suffix_item_id");
        NullableMember secondarySuffixItemId = new("secondary_suffix_item_id");
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
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (member.NameEquals(rarity.Name))
            {
                rarity.Value = member.Value;
            }
            else if (member.NameEquals(vendorValue.Name))
            {
                vendorValue.Value = member.Value;
            }
            else if (member.NameEquals(defaultSkin.Name))
            {
                defaultSkin.Value = member.Value;
            }
            else if (member.NameEquals(gameTypes.Name))
            {
                gameTypes.Value = member.Value;
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
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink.Value = member.Value;
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
                        if (!detail.Value.ValueEquals("ShortBow"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals(damageType.Name))
                    {
                        damageType.Value = detail.Value;
                    }
                    else if (detail.NameEquals(minPower.Name))
                    {
                        minPower.Value = detail.Value;
                    }
                    else if (detail.NameEquals(maxPower.Name))
                    {
                        maxPower.Value = detail.Value;
                    }
                    else if (detail.NameEquals(defense.Name))
                    {
                        defense.Value = detail.Value;
                    }
                    else if (detail.NameEquals(infusionSlots.Name))
                    {
                        infusionSlots.Value = detail.Value;
                    }
                    else if (detail.NameEquals(attributeAdjustment.Name))
                    {
                        attributeAdjustment.Value = detail.Value;
                    }
                    else if (detail.NameEquals(statChoices.Name))
                    {
                        statChoices.Value = detail.Value;
                    }
                    else if (detail.NameEquals(infixUpgrade.Name))
                    {
                        infixUpgrade.Value = detail.Value;
                    }
                    else if (detail.NameEquals(suffixItemId.Name))
                    {
                        suffixItemId.Value = detail.Value;
                    }
                    else if (detail.NameEquals(secondarySuffixItemId.Name))
                    {
                        secondarySuffixItemId.Value = detail.Value;
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

        return new Shortbow
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Level = level.Select(value => value.GetInt32()),
            Rarity = rarity.Select(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            VendorValue = vendorValue.Select(value => value.GetInt32()),
            DefaultSkin = defaultSkin.Select(value => value.GetInt32()),
            GameTypes = gameTypes.SelectMany(value => value.GetEnum<GameType>(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<ItemFlag>(missingMemberBehavior)),
            Restrictions = restrictions.SelectMany(value => value.GetEnum<ItemRestriction>(missingMemberBehavior)),
            ChatLink = chatLink.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetString()),
            DamageType = damageType.Select(value => value.GetEnum<DamageType>(missingMemberBehavior)),
            MinPower = minPower.Select(value => value.GetInt32()),
            MaxPower = maxPower.Select(value => value.GetInt32()),
            Defense = defense.Select(value => value.GetInt32()),
            InfusionSlots =
                infusionSlots.SelectMany(value => value.GetInfusionSlot(missingMemberBehavior)),
            AttributeAdjustment = attributeAdjustment.Select(value => value.GetDouble()),
            StatChoices = statChoices.SelectMany(value => value.GetInt32()),
            Prefix = infixUpgrade.Select(value => value.GetInfixUpgrade(missingMemberBehavior)),
            SuffixItemId = suffixItemId.Select(value => value.GetInt32()),
            SecondarySuffixItemId = secondarySuffixItemId.Select(value => value.GetInt32())
        };
    }
}
