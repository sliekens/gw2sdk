using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class ScepterJson
{
    public static Scepter GetScepter(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
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
        OptionalMember infixUpgrade = "infix_upgrade";
        NullableMember suffixItemId = "suffix_item_id";
        NullableMember secondarySuffixItemId = "secondary_suffix_item_id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Weapon"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (member.Name == level.Name)
            {
                level = member;
            }
            else if (member.Name == rarity.Name)
            {
                rarity = member;
            }
            else if (member.Name == vendorValue.Name)
            {
                vendorValue = member;
            }
            else if (member.Name == defaultSkin.Name)
            {
                defaultSkin = member;
            }
            else if (member.Name == gameTypes.Name)
            {
                gameTypes = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == restrictions.Name)
            {
                restrictions = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == chatLink.Name)
            {
                chatLink = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == "details")
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.Name == "type")
                    {
                        if (!detail.Value.ValueEquals("Scepter"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.Name == damageType.Name)
                    {
                        damageType = detail;
                    }
                    else if (detail.Name == minPower.Name)
                    {
                        minPower = detail;
                    }
                    else if (detail.Name == maxPower.Name)
                    {
                        maxPower = detail;
                    }
                    else if (detail.Name == defense.Name)
                    {
                        defense = detail;
                    }
                    else if (detail.Name == infusionSlots.Name)
                    {
                        infusionSlots = detail;
                    }
                    else if (detail.Name == attributeAdjustment.Name)
                    {
                        attributeAdjustment = detail;
                    }
                    else if (detail.Name == statChoices.Name)
                    {
                        statChoices = detail;
                    }
                    else if (detail.Name == infixUpgrade.Name)
                    {
                        infixUpgrade = detail;
                    }
                    else if (detail.Name == suffixItemId.Name)
                    {
                        suffixItemId = detail;
                    }
                    else if (detail.Name == secondarySuffixItemId.Name)
                    {
                        secondarySuffixItemId = detail;
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

        return new Scepter
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            Level = level.Map(value => value.GetInt32()),
            Rarity = rarity.Map(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            VendorValue = vendorValue.Map(value => value.GetInt32()),
            DefaultSkin = defaultSkin.Map(value => value.GetInt32()),
            GameTypes =
                gameTypes.Map(
                    values => values.GetList(
                        value => value.GetEnum<GameType>(missingMemberBehavior)
                    )
                ),
            Flags =
                flags.Map(
                    values => values.GetList(
                        value => value.GetEnum<ItemFlag>(missingMemberBehavior)
                    )
                ),
            Restrictions =
                restrictions.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<ItemRestriction>(missingMemberBehavior)
                        )
                ),
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetString()),
            DamageType = damageType.Map(value => value.GetEnum<DamageType>(missingMemberBehavior)),
            MinPower = minPower.Map(value => value.GetInt32()),
            MaxPower = maxPower.Map(value => value.GetInt32()),
            Defense = defense.Map(value => value.GetInt32()),
            InfusionSlots =
                infusionSlots.Map(
                    values => values.GetList(value => value.GetInfusionSlot(missingMemberBehavior))
                ),
            AttributeAdjustment = attributeAdjustment.Map(value => value.GetDouble()),
            StatChoices = statChoices.Map(values => values.GetList(value => value.GetInt32())),
            Prefix = infixUpgrade.Map(value => value.GetInfixUpgrade(missingMemberBehavior)),
            SuffixItemId = suffixItemId.Map(value => value.GetInt32()),
            SecondarySuffixItemId = secondarySuffixItemId.Map(value => value.GetInt32())
        };
    }
}
