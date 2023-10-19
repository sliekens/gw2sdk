﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

[PublicAPI]
public static class ItemSetAchievementJson
{
    public static ItemSetAchievement GetItemSetAchievement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        OptionalMember icon = new("icon");
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        RequiredMember requirement = new("requirement");
        RequiredMember lockedText = new("locked_text");
        RequiredMember flags = new("flags");
        RequiredMember tiers = new("tiers");
        OptionalMember prerequisites = new("prerequisites");
        OptionalMember rewards = new("rewards");
        OptionalMember bits = new("bits");
        NullableMember pointCap = new("point_cap");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("ItemSet"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(requirement.Name))
            {
                requirement.Value = member.Value;
            }
            else if (member.NameEquals(lockedText.Name))
            {
                lockedText.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(tiers.Name))
            {
                tiers.Value = member.Value;
            }
            else if (member.NameEquals(prerequisites.Name))
            {
                prerequisites.Value = member.Value;
            }
            else if (member.NameEquals(rewards.Name))
            {
                rewards.Value = member.Value;
            }
            else if (member.NameEquals(bits.Name))
            {
                bits.Value = member.Value;
            }
            else if (member.NameEquals(pointCap.Name))
            {
                pointCap.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ItemSetAchievement
        {
            Id = id.Select(value => value.GetInt32()),
            Icon = icon.Select(value => value.GetString()) ?? "",
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Requirement = requirement.Select(value => value.GetStringRequired()),
            LockedText = lockedText.Select(value => value.GetStringRequired()),
            Flags = flags.SelectMany(value => value.GetEnum<AchievementFlag>(missingMemberBehavior)),
            Tiers = tiers.SelectMany(value => value.GetAchievementTier(missingMemberBehavior)),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Rewards = rewards.SelectMany(
                value => value.GetAchievementReward(missingMemberBehavior)
            ),
            Bits = bits.SelectMany(value => value.GetAchievementBit(missingMemberBehavior)),
            PointCap = pointCap.Select(value => value.GetInt32())
        };
    }
}
