using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Quests;

[PublicAPI]
public static class GoalJson
{
    public static Goal GetGoal(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember active = "active";
        RequiredMember complete = "complete";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(active.Name))
            {
                active = member;
            }
            else if (member.NameEquals(complete.Name))
            {
                complete = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Goal
        {
            Active = active.Select(value => value.GetStringRequired()),
            Complete = complete.Select(value => value.GetStringRequired())
        };
    }
}
