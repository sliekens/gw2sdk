using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Titles;

[PublicAPI]
public static class TitleReader
{
    public static Title Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        OptionalMember<int> achievements = new("achievements");
        NullableMember<int> achievementPointsRequired = new("ap_required");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(achievements.Name))
            {
                achievements = achievements.From(member.Value);
            }
            else if (member.NameEquals(achievementPointsRequired.Name))
            {
                achievementPointsRequired = achievementPointsRequired.From(member.Value);
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
            Id = id.GetValue(),
            Name = name.GetValue(),
            Achievements = achievements.SelectMany(value => value.GetInt32()),
            AchievementPointsRequired = achievementPointsRequired.GetValue()
        };
    }
}
