using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal static class CollectionAchievementJson
{
    public static CollectionAchievement GetCollectionAchievement(this JsonElement json)
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
                if (!member.Value.ValueEquals("ItemSet"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CollectionAchievement
        {
            Id = id.Map(static value => value.GetInt32()),
            IconHref = icon.Map(static value => value.GetString()) ?? "",
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetStringRequired()),
            Requirement = requirement.Map(static value => value.GetStringRequired()),
            LockedText = lockedText.Map(static value => value.GetStringRequired()),
            Flags = flags.Map(static values => values.GetAchievementFlags()),
            Tiers =
                tiers.Map(
                    static values => values.GetList(static value => value.GetAchievementTier())
                ),
            Prerequisites =
                prerequisites.Map(static values => values.GetList(static value => value.GetInt32()))
                ?? Empty.ListOfInt32,
            Rewards =
                rewards.Map(
                    static values => values.GetList(static value => value.GetAchievementReward())
                ),
            Bits = bits.Map(
                static values => values.GetList(static value => value.GetAchievementBit())
            ),
            PointCap = pointCap.Map(static value => value.GetInt32())
        };
    }
}
