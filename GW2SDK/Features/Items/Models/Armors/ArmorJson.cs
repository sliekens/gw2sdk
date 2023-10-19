﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class ArmorJson
{
    public static Armor GetArmor(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        switch (json.GetProperty("details").GetProperty("type").GetString())
        {
            case "Boots":
                return json.GetBoots(missingMemberBehavior);
            case "Coat":
                return json.GetCoat(missingMemberBehavior);
            case "Gloves":
                return json.GetGloves(missingMemberBehavior);
            case "Helm":
                return json.GetHelm(missingMemberBehavior);
            case "HelmAquatic":
                return json.GetHelmAquatic(missingMemberBehavior);
            case "Leggings":
                return json.GetLeggings(missingMemberBehavior);
            case "Shoulders":
                return json.GetShoulders(missingMemberBehavior);
        }

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
        RequiredMember weightClass = "weight_class";
        RequiredMember defense = "defense";
        RequiredMember infusionSlots = "infusion_slots";
        RequiredMember attributeAdjustment = "attribute_adjustment";
        OptionalMember infixUpgrade = "infix_upgrade";
        NullableMember suffixItemId = "suffix_item_id";
        OptionalMember statChoices = "stat_choices";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Armor"))
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
            else if (member.NameEquals(defaultSkin.Name))
            {
                defaultSkin = member;
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
                        if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException(
                                Strings.UnexpectedDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals(weightClass.Name))
                    {
                        weightClass = detail;
                    }
                    else if (detail.NameEquals(defense.Name))
                    {
                        defense = detail;
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

        return new Armor
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Level = level.Select(value => value.GetInt32()),
            Rarity = rarity.Select(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            VendorValue = vendorValue.Select(value => value.GetInt32()),
            DefaultSkin = defaultSkin.Select(value => value.GetInt32()),
            GameTypes = gameTypes.Select(values => values.GetList(value => value.GetEnum<GameType>(missingMemberBehavior))),
            Flags = flags.Select(values => values.GetList(value => value.GetEnum<ItemFlag>(missingMemberBehavior))),
            Restrictions = restrictions.Select(values => values.GetList(value => value.GetEnum<ItemRestriction>(missingMemberBehavior))),
            ChatLink = chatLink.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetString()),
            WeightClass = weightClass.Select(value => value.GetEnum<WeightClass>(missingMemberBehavior)),
            Defense = defense.Select(value => value.GetInt32()),
            InfusionSlots =
                infusionSlots.Select(values => values.GetList(value => value.GetInfusionSlot(missingMemberBehavior))),
            AttributeAdjustment = attributeAdjustment.Select(value => value.GetDouble()),
            StatChoices = statChoices.Select(values => values.GetList(value => value.GetInt32())),
            Prefix = infixUpgrade.Select(value => value.GetInfixUpgrade(missingMemberBehavior)),
            SuffixItemId = suffixItemId.Select(value => value.GetInt32())
        };
    }
}
