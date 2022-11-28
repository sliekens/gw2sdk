using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Quests;

[PublicAPI]
public static class GoalJson
{
    public static Goal GetGoal(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> active = new("active");
        RequiredMember<string> complete = new("complete");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(active.Name))
            {
                active.Value = member.Value;
            }
            else if (member.NameEquals(complete.Name))
            {
                complete.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Goal
        {
            Active = active.GetValue(),
            Complete = complete.GetValue()
        };
    }
}
