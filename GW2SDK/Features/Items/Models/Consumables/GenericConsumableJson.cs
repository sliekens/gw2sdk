using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class GenericConsumableJson
{
    public static GenericConsumable GetGenericConsumable(
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
        var hasEffect = false;

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Consumable"))
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
                        if (!detail.Value.ValueEquals("Generic"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (duration.Match(detail))
                    {
                        duration = detail;
                        hasEffect = true;
                    }
                    else if (applyCount.Match(detail))
                    {
                        applyCount = detail;
                        hasEffect = true;
                    }
                    else if (effectName.Match(detail))
                    {
                        effectName = detail;
                        hasEffect = true;
                    }
                    else if (effectIcon.Match(detail))
                    {
                        effectIcon = detail;
                        hasEffect = true;
                    }
                    else if (effectDescription.Match(detail))
                    {
                        effectDescription = detail;
                        hasEffect = true;
                    }
                    else if (guildUpgradeId.Match(detail))
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

        var (races, professions, bodyTypes) =
            restrictions.Map(value => value.GetRestrictions(missingMemberBehavior));
        return new GenericConsumable
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
            Effect = hasEffect
                ? new Effect
                {
                    Name = effectName.Map(value => value.GetString()) ?? "",
                    Description = effectDescription.Map(value => value.GetString()) ?? "",
                    Duration =
                        duration.Map(value => TimeSpan.FromMilliseconds(value.GetDouble()))
                        ?? TimeSpan.Zero,
                    ApplyCount = applyCount.Map(value => value.GetInt32()) ?? 0,
                    IconHref = effectIcon.Map(value => value.GetString()) ?? ""
                }
                : default,
            GuildUpgradeId = guildUpgradeId.Map(value => value.GetInt32())
        };
    }
}
