using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class UpgradeComponentJson
{
    private static bool IsPvpItem(this in JsonElement json)
    {
        if (!json.TryGetProperty("game_types", out var gameTypes))
        {
            return false;
        }

        if (gameTypes.ValueKind != JsonValueKind.Array)
        {
            return false;
        }

        if (gameTypes.GetArrayLength() == 0)
        {
            return false;
        }

        return gameTypes[0].GetString() == "Pvp";
    }

    private static bool HasFlags(this in JsonElement json, int count)
    {
        if (!json.TryGetProperty("details", out var details))
        {
            return false;
        }

        if (!details.TryGetProperty("flags", out var flags))
        {
            return false;
        }

        if (flags.ValueKind != JsonValueKind.Array)
        {
            return false;
        }

        return flags.GetArrayLength() == count;
    }

    public static UpgradeComponent GetUpgradeComponent(this in JsonElement json)
    {
        if (json.TryGetProperty("details", out var discriminator) && discriminator.TryGetProperty("type", out var subtype))
        {
            switch (subtype.GetString())
            {
                case "Gem":
                    return json.GetGem();
                case "Rune":
                case "Default" when json.IsPvpItem() && json.HasFlags(3):
                    return json.GetRune();
                case "Sigil":
                case "Default" when json.IsPvpItem() && json.HasFlags(19):
                    return json.GetSigil();
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
        RequiredMember upgradeComponentFlags = "flags";
        RequiredMember infusionUpgradeFlags = "infusion_upgrade_flags";
        RequiredMember attributeAdjustment = "attribute_adjustment";
        NullableMember infixUpgradeId = "id";
        OptionalMember infixUpgradeAttributes = "attributes";
        OptionalMember infixUpgradeBuff = "buff";
        RequiredMember suffix = "suffix";
        OptionalMember upgradesInto = "upgrades_into";
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
            else if (upgradesInto.Match(member))
            {
                upgradesInto = member;
            }
            else if (member.NameEquals("details"))
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error
                            && !detail.Value.ValueEquals("Default"))
                        {
                            ThrowHelper.ThrowUnexpectedDiscriminator(detail.Value.GetString());
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

        var iconString = icon.Map(static (in JsonElement value) => value.GetString());
        return new UpgradeComponent
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Description = description.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Level = level.Map(static (in JsonElement value) => value.GetInt32()),
            Rarity = rarity.Map(static (in JsonElement value) => value.GetEnum<Rarity>()),
            VendorValue = vendorValue.Map(static (in JsonElement value) => value.GetInt32()),
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
            UpgradeComponentFlags =
                upgradeComponentFlags.Map(static (in JsonElement values) => values.GetUpgradeComponentFlags()),
            InfusionUpgradeFlags =
                infusionUpgradeFlags.Map(static (in JsonElement values) => values.GetInfusionSlotFlags()),
            AttributeAdjustment = attributeAdjustment.Map(static (in JsonElement value) => value.GetDouble()),
            AttributeCombinationId = infixUpgradeId.Map(static (in JsonElement value) => value.GetInt32()),
            Attributes = infixUpgradeAttributes.Map(static (in JsonElement values) => values.GetAttributes()) ?? new ValueDictionary<Extensible<Hero.AttributeName>, int>(),
            Buff = infixUpgradeBuff.Map(static (in JsonElement value) => value.GetBuff()),
            SuffixName = suffix.Map(static (in JsonElement value) => value.GetStringRequired()),
            UpgradesInto =
                upgradesInto.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetInfusionSlotUpgradePath())
                )
                ?? new ValueList<InfusionSlotUpgradePath>()
        };
    }
}
