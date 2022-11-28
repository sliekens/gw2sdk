using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Achievements;

[PublicAPI]
public static class AchievementJson
{
    public static Achievement GetAchievement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Default":
                return json.GetDefaultAchievement(missingMemberBehavior);
            case "ItemSet":
                return json.GetItemSetAchievement(missingMemberBehavior);
        }

        RequiredMember<int> id = new("id");
        OptionalMember<string> icon = new("icon");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<string> requirement = new("requirement");
        RequiredMember<string> lockedText = new("locked_text");
        RequiredMember<AchievementFlag> flags = new("flags");
        RequiredMember<AchievementTier> tiers = new("tiers");
        OptionalMember<int> prerequisites = new("prerequisites");
        OptionalMember<AchievementReward> rewards = new("rewards");
        OptionalMember<AchievementBit> bits = new("bits");
        NullableMember<int> pointCap = new("point_cap");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
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

        return new Achievement
        {
            Id = id.GetValue(),
            Icon = icon.GetValueOrEmpty(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            Requirement = requirement.GetValue(),
            LockedText = lockedText.GetValue(),
            Flags = flags.GetValues(missingMemberBehavior),
            Tiers = tiers.SelectMany(value => value.GetAchievementTier(missingMemberBehavior)),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Rewards = rewards.SelectMany(
                value => value.GetAchievementReward(missingMemberBehavior)
            ),
            Bits = bits.SelectMany(value => value.GetAchievementBit(missingMemberBehavior)),
            PointCap = pointCap.GetValue()
        };
    }
}
