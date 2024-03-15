using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Titles;

internal static class TitleJson
{
    public static Title GetTitle(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        OptionalMember achievements = "achievements";
        NullableMember achievementPointsRequired = "ap_required";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (achievements.Match(member))
            {
                achievements = member;
            }
            else if (achievementPointsRequired.Match(member))
            {
                achievementPointsRequired = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                if (member.Name == "achievement")
                {
                    // Obsolete because some titles can be unlocked by more than one achievement
                    continue;
                }

                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Title
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Achievements = achievements.Map(values => values.GetList(value => value.GetInt32())),
            AchievementPointsRequired = achievementPointsRequired.Map(value => value.GetInt32())
        };
    }
}
