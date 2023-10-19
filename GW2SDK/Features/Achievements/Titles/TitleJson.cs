using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Titles;

[PublicAPI]
public static class TitleJson
{
    public static Title GetTitle(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        OptionalMember achievements = new("achievements");
        NullableMember achievementPointsRequired = new("ap_required");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(achievements.Name))
            {
                achievements = member;
            }
            else if (member.NameEquals(achievementPointsRequired.Name))
            {
                achievementPointsRequired = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                if (member.NameEquals("achievement"))
                {
                    // Obsolete because some titles can be unlocked by more than one achievement
                    continue;
                }

                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Title
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Achievements = achievements.SelectMany(value => value.GetInt32()),
            AchievementPointsRequired = achievementPointsRequired.Select(value => value.GetInt32())
        };
    }
}
