using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public static class BackpackJson
{
    public static Backpack GetBackpack(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        OptionalMember<string> description = new("description");
        RequiredMember<int> level = new("level");
        RequiredMember<Rarity> rarity = new("rarity");
        RequiredMember<Coin> vendorValue = new("vendor_value");
        RequiredMember<int> defaultSkin = new("default_skin");
        RequiredMember<GameType> gameTypes = new("game_types");
        RequiredMember<ItemFlag> flags = new("flags");
        RequiredMember<ItemRestriction> restrictions = new("restrictions");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<string> icon = new("icon");
        RequiredMember<InfusionSlot> infusionSlots = new("infusion_slots");
        RequiredMember<double> attributeAdjustment = new("attribute_adjustment");
        OptionalMember<InfixUpgrade> infixUpgrade = new("infix_upgrade");
        NullableMember<int> suffixItemId = new("suffix_item_id");
        OptionalMember<int> statChoices = new("stat_choices");
        OptionalMember<ItemUpgrade> upgradesInto = new("upgrades_into");
        OptionalMember<ItemUpgrade> upgradesFrom = new("upgrades_from");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Back"))
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
            else if (member.NameEquals(upgradesInto.Name))
            {
                upgradesInto.Value = member.Value;
            }
            else if (member.NameEquals(upgradesFrom.Name))
            {
                upgradesFrom.Value = member.Value;
            }
            else if (member.NameEquals("details"))
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals(infusionSlots.Name))
                    {
                        infusionSlots.Value = detail.Value;
                    }
                    else if (detail.NameEquals(attributeAdjustment.Name))
                    {
                        attributeAdjustment.Value = detail.Value;
                    }
                    else if (detail.NameEquals(infixUpgrade.Name))
                    {
                        infixUpgrade.Value = detail.Value;
                    }
                    else if (detail.NameEquals(suffixItemId.Name))
                    {
                        suffixItemId.Value = detail.Value;
                    }
                    else if (detail.NameEquals(statChoices.Name))
                    {
                        statChoices.Value = detail.Value;
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

        return new Backpack
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValueOrEmpty(),
            Level = level.GetValue(),
            Rarity = rarity.GetValue(missingMemberBehavior),
            VendorValue = vendorValue.GetValue(),
            DefaultSkin = defaultSkin.GetValue(),
            GameTypes = gameTypes.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Restrictions = restrictions.GetValues(missingMemberBehavior),
            ChatLink = chatLink.GetValue(),
            Icon = icon.GetValueOrNull(),
            InfusionSlots =
                infusionSlots.SelectMany(value => value.GetInfusionSlot(missingMemberBehavior)),
            AttributeAdjustment = attributeAdjustment.GetValue(),
            Prefix = infixUpgrade.Select(value => value.GetInfixUpgrade(missingMemberBehavior)),
            SuffixItemId = suffixItemId.GetValue(),
            StatChoices = statChoices.SelectMany(value => value.GetInt32()),
            UpgradesInto =
                upgradesInto.SelectMany(value => value.GetItemUpgrade(missingMemberBehavior)),
            UpgradesFrom =
                upgradesFrom.SelectMany(value => value.GetItemUpgrade(missingMemberBehavior))
        };
    }
}
