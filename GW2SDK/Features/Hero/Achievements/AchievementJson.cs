using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

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
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error
                    && !member.Value.ValueEquals("Default"))
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Achievement
        {
            Id = id.Map(value => value.GetInt32()),
            IconHref = icon.Map(value => value.GetString()) ?? "",
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Requirement = requirement.Map(value => value.GetStringRequired()),
            LockedText = lockedText.Map(value => value.GetStringRequired()),
            Flags = flags.Map(values => values.GetAchievementFlags()),
            Tiers =
                tiers.Map(
                    values => values.GetList(
                        value => value.GetAchievementTier(missingMemberBehavior)
                    )
                ),
            Prerequisites =
                prerequisites.Map(values => values.GetList(value => value.GetInt32()))
                ?? Empty.ListOfInt32,
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
