using System.Text.Json;
using GuildWars2.Json;

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
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(requirement.Name))
            {
                requirement = member;
            }
            else if (member.NameEquals(lockedText.Name))
            {
                lockedText = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(tiers.Name))
            {
                tiers = member;
            }
            else if (member.NameEquals(prerequisites.Name))
            {
                prerequisites = member;
            }
            else if (member.NameEquals(rewards.Name))
            {
                rewards = member;
            }
            else if (member.NameEquals(bits.Name))
            {
                bits = member;
            }
            else if (member.NameEquals(pointCap.Name))
            {
                pointCap = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Achievement
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
