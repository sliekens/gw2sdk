using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class ImmediateConsumableJson
{
    public static ImmediateConsumable GetImmediateConsumable(
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
        NullableMember duration = "duration_ms";
        NullableMember applyCount = "apply_count";
        OptionalMember effectName = "name";
        OptionalMember effectIcon = "icon";
        OptionalMember effectDescription = "description";
        NullableMember guildUpgradeId = "guild_upgrade_id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Consumable"))
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
                        if (!detail.Value.ValueEquals("Immediate"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals(duration.Name))
                    {
                        duration = detail;
                    }
                    else if (detail.NameEquals(applyCount.Name))
                    {
                        applyCount = detail;
                    }
                    else if (detail.NameEquals(effectName.Name))
                    {
                        effectName = detail;
                    }
                    else if (detail.NameEquals(effectIcon.Name))
                    {
                        effectIcon = detail;
                    }
                    else if (detail.NameEquals(effectDescription.Name))
                    {
                        effectDescription = detail;
                    }
                    else if (detail.NameEquals(guildUpgradeId.Name))
                    {
                        guildUpgradeId = detail;
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

        return new ImmediateConsumable
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
            Duration = duration.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            ApplyCount = applyCount.Select(value => value.GetInt32()),
            EffectName = effectName.Select(value => value.GetString()) ?? "",
            EffectIcon = effectIcon.Select(value => value.GetString()),
            EffectDescription = effectDescription.Select(value => value.GetString()) ?? "",
            GuildUpgradeId = guildUpgradeId.Select(value => value.GetInt32())
        };
    }
}
