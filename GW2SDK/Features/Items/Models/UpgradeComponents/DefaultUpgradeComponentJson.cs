using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class DefaultUpgradeComponentJson
{
    public static DefaultUpgradeComponent GetDefaultUpgradeComponent(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
        RequiredMember infixUpgrade = "infix_upgrade";
        RequiredMember suffix = "suffix";
        OptionalMember bonuses = "bonuses";
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
            else if (member.NameEquals("details"))
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (!detail.Value.ValueEquals("Default"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals(upgradeComponentFlags.Name))
                    {
                        upgradeComponentFlags = detail;
                    }
                    else if (detail.NameEquals(infusionUpgradeFlags.Name))
                    {
                        infusionUpgradeFlags = detail;
                    }
                    else if (detail.NameEquals(attributeAdjustment.Name))
                    {
                        attributeAdjustment = detail;
                    }
                    else if (detail.NameEquals(infixUpgrade.Name))
                    {
                        infixUpgrade = detail;
                    }
                    else if (detail.NameEquals(suffix.Name))
                    {
                        suffix = detail;
                    }
                    else if (detail.NameEquals(bonuses.Name))
                    {
                        bonuses = detail;
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

        return new DefaultUpgradeComponent
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Level = level.Select(value => value.GetInt32()),
            Rarity = rarity.Select(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            VendorValue = vendorValue.Select(value => value.GetInt32()),
            GameTypes = gameTypes.Select(values => values.GetList(value => value.GetEnum<GameType>(missingMemberBehavior))),
            Flags = flags.Select(values => values.GetList(value => value.GetEnum<ItemFlag>(missingMemberBehavior))),
            Restrictions = restrictions.Select(values => values.GetList(value => value.GetEnum<ItemRestriction>(missingMemberBehavior))),
            ChatLink = chatLink.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetString()),
            UpgradeComponentFlags = upgradeComponentFlags.Select(values => values.GetList(value => value.GetEnum<UpgradeComponentFlag>(missingMemberBehavior))),
            InfusionUpgradeFlags = infusionUpgradeFlags.Select(values => values.GetList(value => value.GetEnum<InfusionSlotFlag>(missingMemberBehavior))),
            AttributeAdjustment = attributeAdjustment.Select(value => value.GetDouble()),
            Suffix = infixUpgrade.Select(value => value.GetInfixUpgrade(missingMemberBehavior)),
            SuffixName = suffix.Select(value => value.GetStringRequired()),
            Bonuses = bonuses.Select(values => values.GetList(value => value.GetStringRequired()))
        };
    }
}
