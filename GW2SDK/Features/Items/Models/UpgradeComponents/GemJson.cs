using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public static class GemJson
{
    public static Gem GetGem(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        OptionalMember<string> description = new("description");
        RequiredMember<int> level = new("level");
        RequiredMember<Rarity> rarity = new("rarity");
        RequiredMember<Coin> vendorValue = new("vendor_value");
        RequiredMember<GameType> gameTypes = new("game_types");
        RequiredMember<ItemFlag> flags = new("flags");
        RequiredMember<ItemRestriction> restrictions = new("restrictions");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<string> icon = new("icon");
        RequiredMember<UpgradeComponentFlag> upgradeComponentFlags = new("flags");
        RequiredMember<InfusionSlotFlag> infusionUpgradeFlags = new("infusion_upgrade_flags");
        RequiredMember<double> attributeAdjustment = new("attribute_adjustment");
        RequiredMember<InfixUpgrade> infixUpgrade = new("infix_upgrade");
        RequiredMember<string> suffix = new("suffix");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("UpgradeComponent"))
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
                        if (!detail.Value.ValueEquals("Gem"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals(upgradeComponentFlags.Name))
                    {
                        upgradeComponentFlags.Value = detail.Value;
                    }
                    else if (detail.NameEquals(infusionUpgradeFlags.Name))
                    {
                        infusionUpgradeFlags.Value = detail.Value;
                    }
                    else if (detail.NameEquals(attributeAdjustment.Name))
                    {
                        attributeAdjustment.Value = detail.Value;
                    }
                    else if (detail.NameEquals(infixUpgrade.Name))
                    {
                        infixUpgrade.Value = detail.Value;
                    }
                    else if (detail.NameEquals(suffix.Name))
                    {
                        suffix.Value = detail.Value;
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

        return new Gem
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValueOrEmpty(),
            Level = level.GetValue(),
            Rarity = rarity.GetValue(missingMemberBehavior),
            VendorValue = vendorValue.GetValue(),
            GameTypes = gameTypes.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Restrictions = restrictions.GetValues(missingMemberBehavior),
            ChatLink = chatLink.GetValue(),
            Icon = icon.GetValueOrNull(),
            UpgradeComponentFlags = upgradeComponentFlags.GetValues(missingMemberBehavior),
            InfusionUpgradeFlags = infusionUpgradeFlags.GetValues(missingMemberBehavior),
            AttributeAdjustment = attributeAdjustment.GetValue(),
            Suffix = infixUpgrade.Select(value => value.GetInfixUpgrade(missingMemberBehavior)),
            SuffixName = suffix.GetValue()
        };
    }
}
