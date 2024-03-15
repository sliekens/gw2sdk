﻿using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class UpgradeComponentJson
{
    private static bool IsPvpItem(this JsonElement json) =>
        json.GetProperty("game_types")[0].GetString() == "Pvp";

    private static bool HasFlags(this JsonElement json, int count) =>
        json.GetProperty("details").GetProperty("flags").GetArrayLength() == count;

    public static UpgradeComponent GetUpgradeComponent(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("details").GetProperty("type").GetString())
        {
            case "Gem":
                return json.GetGem(missingMemberBehavior);
            case "Rune":
            case "Default" when json.IsPvpItem() && json.HasFlags(3):
                return json.GetRune(missingMemberBehavior);
            case "Sigil":
            case "Default" when json.IsPvpItem() && json.HasFlags(19):
                return json.GetSigil(missingMemberBehavior);
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
        RequiredMember upgradeComponentFlags = "flags";
        RequiredMember infusionUpgradeFlags = "infusion_upgrade_flags";
        RequiredMember attributeAdjustment = "attribute_adjustment";
        NullableMember infixUpgradeId = "id";
        OptionalMember infixUpgradeAttributes = "attributes";
        OptionalMember infixUpgradeBuff = "buff";
        RequiredMember suffix = "suffix";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("UpgradeComponent"))
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
            else if (member.Name == "details")
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.Name == "type")
                    {
                        if (missingMemberBehavior == MissingMemberBehavior.Error
                            && !detail.Value.ValueEquals("Default"))
                        {
                            throw new InvalidOperationException(
                                Strings.UnexpectedDiscriminator(detail.Value.GetString())
                            );
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
                    else if (detail.Name == "infix_upgrade")
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
                            else if (missingMemberBehavior == MissingMemberBehavior.Error)
                            {
                                throw new InvalidOperationException(
                                    Strings.UnexpectedMember(infix.Name)
                                );
                            }
                        }
                    }
                    else if (suffix.Match(detail))
                    {
                        suffix = detail;
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
        return new UpgradeComponent
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
            UpgradeComponentFlags =
                upgradeComponentFlags.Map(values => values.GetUpgradeComponentFlags()),
            InfusionUpgradeFlags =
                infusionUpgradeFlags.Map(values => values.GetInfusionSlotFlags()),
            AttributeAdjustment = attributeAdjustment.Map(value => value.GetDouble()),
            AttributeCombinationId = infixUpgradeId.Map(value => value.GetInt32()),
            Attributes =
                infixUpgradeAttributes.Map(values => values.GetAttributes(missingMemberBehavior))
                ?? new Dictionary<AttributeName, int>(0),
            Buff = infixUpgradeBuff.Map(value => value.GetBuff(missingMemberBehavior)),
            SuffixName = suffix.Map(value => value.GetStringRequired())
        };
    }
}
