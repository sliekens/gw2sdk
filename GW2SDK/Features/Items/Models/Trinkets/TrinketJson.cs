using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class TrinketJson
{
    public static Trinket GetTrinket(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("details").GetProperty("type").GetString())
        {
            case "Accessory":
                return json.GetAccessory(missingMemberBehavior);
            case "Amulet":
                return json.GetAmulet(missingMemberBehavior);
            case "Ring":
                return json.GetRing(missingMemberBehavior);
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
        OptionalMember infixUpgrade = "infix_upgrade";
        NullableMember suffixItemId = "suffix_item_id";
        OptionalMember statChoices = "stat_choices";
        OptionalMember upgradesInto = "upgrades_into";
        OptionalMember upgradesFrom = "upgrades_from";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Trinket"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(level.Name))
            {
                level = member;
            }
            else if (member.NameEquals(rarity.Name))
            {
                rarity = member;
            }
            else if (member.NameEquals(vendorValue.Name))
            {
                vendorValue = member;
            }
            else if (member.NameEquals(gameTypes.Name))
            {
                gameTypes = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(restrictions.Name))
            {
                restrictions = member;
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(upgradesInto.Name))
            {
                upgradesInto = member;
            }
            else if (member.NameEquals(upgradesFrom.Name))
            {
                upgradesFrom = member;
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
                    else if (detail.NameEquals(infusionSlots.Name))
                    {
                        infusionSlots = detail;
                    }
                    else if (detail.NameEquals(attributeAdjustment.Name))
                    {
                        attributeAdjustment = detail;
                    }
                    else if (detail.NameEquals(infixUpgrade.Name))
                    {
                        infixUpgrade = detail;
                    }
                    else if (detail.NameEquals(suffixItemId.Name))
                    {
                        suffixItemId = detail;
                    }
                    else if (detail.NameEquals(statChoices.Name))
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

        return new Trinket
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            Level = level.Map(value => value.GetInt32()),
            Rarity = rarity.Map(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            VendorValue = vendorValue.Map(value => value.GetInt32()),
            GameTypes = gameTypes.Map(values => values.GetList(value => value.GetEnum<GameType>(missingMemberBehavior))),
            Flags = flags.Map(values => values.GetList(value => value.GetEnum<ItemFlag>(missingMemberBehavior))),
            Restrictions = restrictions.Map(values => values.GetList(value => value.GetEnum<ItemRestriction>(missingMemberBehavior))),
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetString()),
            InfusionSlots =
                infusionSlots.Map(values => values.GetList(value => value.GetInfusionSlot(missingMemberBehavior))),
            AttributeAdjustment = attributeAdjustment.Map(value => value.GetDouble()),
            StatChoices = statChoices.Map(values => values.GetList(value => value.GetInt32())),
            Prefix = infixUpgrade.Map(value => value.GetInfixUpgrade(missingMemberBehavior)),
            SuffixItemId = suffixItemId.Map(value => value.GetInt32()),
            UpgradesInto =
                upgradesInto.Map(values => values.GetList(value => value.GetItemUpgrade(missingMemberBehavior))),
            UpgradesFrom =
                upgradesFrom.Map(values => values.GetList(value => value.GetItemUpgrade(missingMemberBehavior)))
        };
    }
}
