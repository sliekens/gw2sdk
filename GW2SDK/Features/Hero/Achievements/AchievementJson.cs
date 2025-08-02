using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal static class AchievementJson
{
    public static Achievement GetAchievement(this in JsonElement json)
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "ItemSet":
                    return json.GetCollectionAchievement();
            }
        }

        RequiredMember id = "id";
        OptionalMember icon = "icon";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember requirement = "requirement";
        RequiredMember lockedText = "locked_text";
        RequiredMember flags = "flags";
        RequiredMember tiers = "tiers";
        OptionalMember prerequisites = "prerequisites";
        OptionalMember rewards = "rewards";
        OptionalMember bits = "bits";
        NullableMember pointCap = "point_cap";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error
                    && !member.Value.ValueEquals("Default"))
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (requirement.Match(member))
            {
                requirement = member;
            }
            else if (lockedText.Match(member))
            {
                lockedText = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (tiers.Match(member))
            {
                tiers = member;
            }
            else if (prerequisites.Match(member))
            {
                prerequisites = member;
            }
            else if (rewards.Match(member))
            {
                rewards = member;
            }
            else if (bits.Match(member))
            {
                bits = member;
            }
            else if (pointCap.Match(member))
            {
                pointCap = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static (in JsonElement value) => value.GetString()) ?? "";
        return new Achievement
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString, UriKind.RelativeOrAbsolute) : null,
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Description = description.Map(static (in JsonElement value) => value.GetStringRequired()),
            Requirement = requirement.Map(static (in JsonElement value) => value.GetStringRequired()),
            LockedText = lockedText.Map(static (in JsonElement value) => value.GetStringRequired()),
            Flags = flags.Map(static (in JsonElement values) => values.GetAchievementFlags()),
            Tiers =
                tiers.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetAchievementTier())
                ),
            Prerequisites =
                prerequisites.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32()))
                ?? new Collections.ValueList<int>(),
            Rewards =
                rewards.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetAchievementReward())
                ),
            Bits = bits.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetAchievementBit())
            ),
            PointCap = pointCap.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
