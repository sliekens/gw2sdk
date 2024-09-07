﻿using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class RuneJson
{
    public static Rune GetRune(this JsonElement json)
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
        RequiredMember upgradeComponentFlags = "flags";
        RequiredMember infusionUpgradeFlags = "infusion_upgrade_flags";
        RequiredMember attributeAdjustment = "attribute_adjustment";
        NullableMember infixUpgradeId = "id";
        OptionalMember infixUpgradeAttributes = "attributes";
        OptionalMember infixUpgradeBuff = "buff";
        RequiredMember suffix = "suffix";
        OptionalMember bonuses = "bonuses";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("UpgradeComponent"))
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
                        // PvP runes have type set to Default :(
                        if (!detail.Value.ValueEquals("Rune")
                            && !detail.Value.ValueEquals("Default"))
                        {
                            ThrowHelper.ThrowInvalidDiscriminator(detail.Value.GetString());
                        }
                    }
                    else if (upgradeComponentFlags.Match(detail))
                    {
                        upgradeComponentFlags = detail;
                    }
                    else if (infusionUpgradeFlags.Match(detail))
                    {
                        infusionUpgradeFlags = detail;
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
                    else if (suffix.Match(detail))
                    {
                        suffix = detail;
                    }
                    else if (bonuses.Match(detail))
                    {
                        bonuses = detail;
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

        return new Rune
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetString()) ?? "",
            Level = level.Map(static value => value.GetInt32()),
            Rarity = rarity.Map(static value => value.GetEnum<Rarity>()),
            VendorValue = vendorValue.Map(static value => value.GetInt32()),
            GameTypes =
                gameTypes.Map(
                    static values => values.GetList(static value => value.GetEnum<GameType>())
                ),
            Flags = flags.Map(static values => values.GetItemFlags()),
            Restrictions = restrictions.Map(static value => value.GetItemRestriction()),
            ChatLink = chatLink.Map(static value => value.GetStringRequired()),
            IconHref = icon.Map(static value => value.GetString()),
            UpgradeComponentFlags =
                upgradeComponentFlags.Map(static values => values.GetUpgradeComponentFlags()),
            InfusionUpgradeFlags =
                infusionUpgradeFlags.Map(static values => values.GetInfusionSlotFlags()),
            AttributeAdjustment = attributeAdjustment.Map(static value => value.GetDouble()),
            AttributeCombinationId = infixUpgradeId.Map(static value => value.GetInt32()),
            Attributes =
                infixUpgradeAttributes.Map(static values => values.GetAttributes())
                ?? new Dictionary<Extensible<AttributeName>, int>(0),
            Buff = infixUpgradeBuff.Map(static value => value.GetBuff()),
            SuffixName = suffix.Map(static value => value.GetStringRequired()),
            Bonuses = bonuses.Map(
                static values => values.GetList(static value => value.GetStringRequired())
            )
        };
    }
}
