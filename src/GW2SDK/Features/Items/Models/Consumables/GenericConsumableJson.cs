using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class GenericConsumableJson
{
    public static GenericConsumable GetGenericConsumable(this in JsonElement json)
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
        bool hasEffect = false;

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Consumable"))
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
            else if (member.NameEquals("details"))
            {
                foreach (JsonProperty detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (!detail.Value.ValueEquals("Generic"))
                        {
                            ThrowHelper.ThrowInvalidDiscriminator(detail.Value.GetString());
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

        string? iconString = icon.Map(static (in JsonElement value) => value.GetString());
        return new GenericConsumable
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
            Effect = hasEffect
                ? new Effect
                {
                    Name = effectName.Map(static (in JsonElement value) => value.GetString()) ?? "",
                    Description = effectDescription.Map(static (in JsonElement value) => value.GetString()) ?? "",
                    Duration =
                        duration.Map(static (in JsonElement value) => TimeSpan.FromMilliseconds(value.GetDouble()))
                        ?? TimeSpan.Zero,
                    ApplyCount = applyCount.Map(static (in JsonElement value) => value.GetInt32()) ?? 0,
#pragma warning disable CS0618 // Suppress obsolete warning
                    IconHref = effectIcon.Map(static (in JsonElement value) => value.GetString()) ?? "",
#pragma warning restore CS0618
                    IconUrl = effectIcon.Map(static (in JsonElement value) =>
                    {
                        string? href = value.GetString();
                        return !string.IsNullOrEmpty(href) ? new Uri(href) : null;
                    })
                }
                : default,
            GuildUpgradeId = guildUpgradeId.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
