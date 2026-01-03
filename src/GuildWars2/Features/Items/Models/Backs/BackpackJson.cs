using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class BackpackJson
{
    public static Backpack GetBackpack(this in JsonElement json)
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
        RequiredMember infusionSlots = "infusion_slots";
        RequiredMember attributeAdjustment = "attribute_adjustment";
        NullableMember infixUpgradeId = "id";
        OptionalMember infixUpgradeAttributes = "attributes";
        OptionalMember infixUpgradeBuff = "buff";
        NullableMember suffixItemId = "suffix_item_id";
        OptionalMember statChoices = "stat_choices";
        OptionalMember upgradesInto = "upgrades_into";
        OptionalMember upgradesFrom = "upgrades_from";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Back"))
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
            else if (upgradesInto.Match(member))
            {
                upgradesInto = member;
            }
            else if (upgradesFrom.Match(member))
            {
                upgradesFrom = member;
            }
            else if (member.NameEquals("details"))
            {
                foreach (JsonProperty detail in member.Value.EnumerateObject())
                {
                    if (infusionSlots.Match(detail))
                    {
                        infusionSlots = detail;
                    }
                    else if (attributeAdjustment.Match(detail))
                    {
                        attributeAdjustment = detail;
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

        string? iconString = icon.Map(static (in value) => value.GetString());
        return new Backpack
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetString()) ?? "",
            Level = level.Map(static (in value) => value.GetInt32()),
            Rarity = rarity.Map(static (in value) => value.GetEnum<Rarity>()),
            VendorValue = vendorValue.Map(static (in value) => value.GetInt32()),
            DefaultSkinId = defaultSkin.Map(static (in value) => value.GetInt32()),
            GameTypes =
                gameTypes.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetEnum<GameType>())
                ),
            Flags = flags.Map(static (in values) => values.GetItemFlags()),
            Restrictions = restrictions.Map(static (in value) => value.GetItemRestriction()),
            ChatLink = chatLink.Map(static (in value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null,
            InfusionSlots =
                infusionSlots.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetInfusionSlot())
                ),
            AttributeAdjustment = attributeAdjustment.Map(static (in value) => value.GetDouble()),
            AttributeCombinationId = infixUpgradeId.Map(static (in value) => value.GetInt32()),
            Attributes = infixUpgradeAttributes.Map(static (in values) => values.GetAttributes()) ?? [],
            Buff = infixUpgradeBuff.Map(static (in value) => value.GetBuff()),
            SuffixItemId = suffixItemId.Map(static (in value) => value.GetInt32()),
            StatChoices =
                statChoices.Map(static (in values) => values.GetList(static (in value) => value.GetInt32()))
                ?? [],
            UpgradesInto =
                upgradesInto.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetInfusionSlotUpgradePath())
                )
                ?? [],
            UpgradesFrom = upgradesFrom.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetInfusionSlotUpgradeSource())
                )
                ?? []
        };
    }
}
