using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

internal static class AchievementJson
{
    public static Achievement GetAchievement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "ItemSet":
                return json.GetCollectionAchievement(missingMemberBehavior);
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
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Default"))
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (member.Name == requirement.Name)
            {
                requirement = member;
            }
            else if (member.Name == lockedText.Name)
            {
                lockedText = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == tiers.Name)
            {
                tiers = member;
            }
            else if (member.Name == prerequisites.Name)
            {
                prerequisites = member;
            }
            else if (member.Name == rewards.Name)
            {
                rewards = member;
            }
            else if (member.Name == bits.Name)
            {
                bits = member;
            }
            else if (member.Name == pointCap.Name)
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
            Id = id.Map(value => value.GetInt32()),
            Icon = icon.Map(value => value.GetString()) ?? "",
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Requirement = requirement.Map(value => value.GetStringRequired()),
            LockedText = lockedText.Map(value => value.GetStringRequired()),
            Flags =
                flags.Map(
                    values => values.GetAchievementFlags(missingMemberBehavior)
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
