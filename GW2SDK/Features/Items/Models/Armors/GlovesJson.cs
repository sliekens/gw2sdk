using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class GlovesJson
{
    public static Gloves GetGloves(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = new("name");
        OptionalMember description = new("description");
        RequiredMember level = new("level");
        RequiredMember rarity = new("rarity");
        RequiredMember vendorValue = new("vendor_value");
        RequiredMember defaultSkin = new("default_skin");
        RequiredMember gameTypes = new("game_types");
        RequiredMember flags = new("flags");
        RequiredMember restrictions = new("restrictions");
        RequiredMember id = new("id");
        RequiredMember chatLink = new("chat_link");
        OptionalMember icon = new("icon");
        RequiredMember weightClass = new("weight_class");
        RequiredMember defense = new("defense");
        RequiredMember infusionSlots = new("infusion_slots");
        RequiredMember attributeAdjustment = new("attribute_adjustment");
        OptionalMember infixUpgrade = new("infix_upgrade");
        NullableMember suffixItemId = new("suffix_item_id");
        OptionalMember statChoices = new("stat_choices");
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
                        if (!detail.Value.ValueEquals("Gloves"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
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

        return new Gloves
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Level = level.Select(value => value.GetInt32()),
            Rarity = rarity.Select(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            VendorValue = vendorValue.Select(value => value.GetInt32()),
            DefaultSkin = defaultSkin.Select(value => value.GetInt32()),
            GameTypes = gameTypes.SelectMany(value => value.GetEnum<GameType>(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<ItemFlag>(missingMemberBehavior)),
            Restrictions = restrictions.SelectMany(value => value.GetEnum<ItemRestriction>(missingMemberBehavior)),
            ChatLink = chatLink.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetString()),
            WeightClass = weightClass.Select(value => value.GetEnum<WeightClass>(missingMemberBehavior)),
            Defense = defense.Select(value => value.GetInt32()),
            InfusionSlots =
                infusionSlots.SelectMany(value => value.GetInfusionSlot(missingMemberBehavior)),
            AttributeAdjustment = attributeAdjustment.Select(value => value.GetDouble()),
            StatChoices = statChoices.SelectMany(value => value.GetInt32()),
            Prefix = infixUpgrade.Select(value => value.GetInfixUpgrade(missingMemberBehavior)),
            SuffixItemId = suffixItemId.Select(value => value.GetInt32())
        };
    }
}
