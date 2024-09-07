using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class UtilityJson
{
    public static Utility GetUtility(this JsonElement json)
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
        var hasEffect = false;

        foreach (var member in json.EnumerateObject())
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
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (!detail.Value.ValueEquals("Utility"))
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

        return new Utility
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetString()) ?? "",
            Level = level.Map(static value => value.GetInt32()),
            Rarity = rarity.Map(static value => value.GetEnum<Rarity>()),
            VendorValue = vendorValue.Map(static value => value.GetInt32()),
            GameTypes =
                gameTypes.Map(
                    static values => values.GetList(static value => value.GetEnum<GameType>())
                ),
            Flags = flags.Map(static values => values.GetItemFlags()),
            Restrictions = restrictions.Map(static value => value.GetItemRestriction()),
            ChatLink = chatLink.Map(static value => value.GetStringRequired()),
            IconHref = icon.Map(static value => value.GetString()),
            Effect = hasEffect
                ? new Effect
                {
                    Name = effectName.Map(static value => value.GetString()) ?? "",
                    Description = effectDescription.Map(static value => value.GetString()) ?? "",
                    Duration =
                        duration.Map(static value => TimeSpan.FromMilliseconds(value.GetDouble()))
                        ?? TimeSpan.Zero,
                    ApplyCount = applyCount.Map(static value => value.GetInt32()) ?? 0,
                    IconHref = effectIcon.Map(static value => value.GetString()) ?? ""
                }
                : default
        };
    }
}
