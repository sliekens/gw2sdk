using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

[PublicAPI]
public static class DefaultAchievementJson
{
    public static DefaultAchievement GetDefaultAchievement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
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
                if (!member.Value.ValueEquals("Default"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
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

        return new DefaultAchievement
        {
            Id = id.Map(value => value.GetInt32()),
            Icon = icon.Map(value => value.GetString()) ?? "",
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Requirement = requirement.Map(value => value.GetStringRequired()),
            LockedText = lockedText.Map(value => value.GetStringRequired()),
            Flags =
                flags.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<AchievementFlag>(missingMemberBehavior)
                        )
                ),
            Tiers =
                tiers.Map(
                    values => values.GetList(
                        value => value.GetAchievementTier(missingMemberBehavior)
                    )
                ),
            Prerequisites = prerequisites.Map(values => values.GetList(value => value.GetInt32())),
            Rewards = rewards.Map(
                values => values.GetList(value => value.GetAchievementReward(missingMemberBehavior))
            ),
            Bits = bits.Map(
                values => values.GetList(value => value.GetAchievementBit(missingMemberBehavior))
            ),
            PointCap = pointCap.Map(value => value.GetInt32())
        };
    }
}
