﻿using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class RingJson
{
    public static Ring GetRing(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember level = "level";
        RequiredMember rarity = "rarity";
        RequiredMember vendorValue = "vendor_value";
        RequiredMember gameTypes = "game_types";
        RequiredMember flags = "flags";
        RequiredMember restrictions = "restrictions";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        OptionalMember icon = "icon";
        RequiredMember infusionSlots = "infusion_slots";
        RequiredMember attributeAdjustment = "attribute_adjustment";
        NullableMember infixUpgradeId = "id";
        OptionalMember infixUpgradeAttributes = "attributes";
        OptionalMember infixUpgradeBuff = "buff";
        NullableMember suffixItemId = "suffix_item_id";
        OptionalMember statChoices = "stat_choices";
        OptionalMember upgradesInto = "upgrades_into";
        OptionalMember upgradesFrom = "upgrades_from";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Trinket"))
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
            else if (member.Name == upgradesInto.Name)
            {
                upgradesInto = member;
            }
            else if (member.Name == upgradesFrom.Name)
            {
                upgradesFrom = member;
            }
            else if (member.Name == "details")
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.Name == "type")
                    {
                        if (!detail.Value.ValueEquals("Ring"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.Name == infusionSlots.Name)
                    {
                        infusionSlots = detail;
                    }
                    else if (detail.Name == attributeAdjustment.Name)
                    {
                        attributeAdjustment = detail;
                    }
                    else if (detail.Name == "infix_upgrade")
                    {
                        foreach (var infix in detail.Value.EnumerateObject())
                        {
                            if (infix.Name == infixUpgradeId.Name)
                            {
                                infixUpgradeId = infix;
                            }
                            else if (infix.Name == infixUpgradeAttributes.Name)
                            {
                                infixUpgradeAttributes = infix;
                            }
                            else if (infix.Name == infixUpgradeBuff.Name)
                            {
                                infixUpgradeBuff = infix;
                            }
                            else if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedMember(infix.Name)
                                );
                            }
                        }
                    }
                    else if (detail.Name == suffixItemId.Name)
                    {
                        suffixItemId = detail;
                    }
                    else if (detail.Name == statChoices.Name)
                    {
                        statChoices = detail;
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

        var (races, professions, bodyTypes) =
            restrictions.Map(value => value.GetRestrictions(missingMemberBehavior));
        return new Ring
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            Level = level.Map(value => value.GetInt32()),
            Rarity = rarity.Map(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            VendorValue = vendorValue.Map(value => value.GetInt32()),
            GameTypes =
                gameTypes.Map(
                    values => values.GetList(
                        value => value.GetEnum<GameType>(missingMemberBehavior)
                    )
                ),
            Flags = flags.Map(values => values.GetItemFlags()),
            Races = races,
            Professions = professions,
            BodyTypes = bodyTypes,
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetString()),
            InfusionSlots =
                infusionSlots.Map(
                    values => values.GetList(value => value.GetInfusionSlot(missingMemberBehavior))
                ),
            AttributeAdjustment = attributeAdjustment.Map(value => value.GetDouble()),
            StatChoices =
                statChoices.Map(values => values.GetList(value => value.GetInt32()))
                ?? Empty.ListOfInt32,
            AttributeCombinationId = infixUpgradeId.Map(value => value.GetInt32()),
            Attributes =
                infixUpgradeAttributes.Map(values => values.GetAttributes(missingMemberBehavior))
                ?? new Dictionary<AttributeName, int>(0),
            Buff = infixUpgradeBuff.Map(value => value.GetBuff(missingMemberBehavior)),
            SuffixItemId = suffixItemId.Map(value => value.GetInt32()),
            UpgradesInto =
                upgradesInto.Map(
                    values => values.GetList(value => value.GetItemUpgrade(missingMemberBehavior))
                )
                ?? Empty.List<ItemUpgrade>(),
            UpgradesFrom = upgradesFrom.Map(
                    values => values.GetList(value => value.GetItemUpgrade(missingMemberBehavior))
                )
                ?? Empty.List<ItemUpgrade>()
        };
    }
}
