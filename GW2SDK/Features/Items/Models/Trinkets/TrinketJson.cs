﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class TrinketJson
{
    public static Trinket GetTrinket(this JsonElement json)
    {
        if (json.TryGetProperty("details", out var discriminator))
        {
            if (discriminator.TryGetProperty("type", out var subtype))
            {
                switch (subtype.GetString())
                {
                    case "Accessory":
                        return json.GetAccessory();
                    case "Amulet":
                        return json.GetAmulet();
                    case "Ring":
                        return json.GetRing();
                }
            }
        }

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
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Trinket"))
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
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            ThrowHelper.ThrowUnexpectedDiscriminator(detail.Value.GetString());
                        }
                    }
                    else if (infusionSlots.Match(detail))
                    {
                        infusionSlots = detail;
                    }
                    else if (attributeAdjustment.Match(detail))
                    {
                        attributeAdjustment = detail;
                    }
                    else if (detail.NameEquals("infix_upgrade"))
                    {
                        foreach (var infix in detail.Value.EnumerateObject())
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
                    else if (statChoices.Match(detail))
                    {
                        statChoices = detail;
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

        var iconString = icon.Map(static value => value.GetString());
        return new Trinket
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetString()) ?? "",
            Level = level.Map(static value => value.GetInt32()),
            Rarity = rarity.Map(static value => value.GetEnum<Rarity>()),
            VendorValue = vendorValue.Map(static value => value.GetInt32()),
            GameTypes =
                gameTypes.Map(static values =>
                    values.GetList(static value => value.GetEnum<GameType>())
                ),
            Flags = flags.Map(static values => values.GetItemFlags()),
            Restrictions = restrictions.Map(static value => value.GetItemRestriction()),
            ChatLink = chatLink.Map(static value => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null,
            InfusionSlots =
                infusionSlots.Map(static values =>
                    values.GetList(static value => value.GetInfusionSlot())
                ),
            AttributeAdjustment = attributeAdjustment.Map(static value => value.GetDouble()),
            StatChoices =
                statChoices.Map(static values => values.GetList(static value => value.GetInt32()))
                ?? [],
            AttributeCombinationId = infixUpgradeId.Map(static value => value.GetInt32()),
            Attributes = infixUpgradeAttributes.Map(static values => values.GetAttributes()) ?? [],
            Buff = infixUpgradeBuff.Map(static value => value.GetBuff()),
            SuffixItemId = suffixItemId.Map(static value => value.GetInt32())
        };
    }
}
