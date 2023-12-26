using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class ObjectiveJson
{
    public static Objective GetObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember active = "active";
        RequiredMember complete = "complete";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == active.Name)
            {
                active = member;
            }
            else if (member.Name == complete.Name)
            {
                complete = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Objective
        {
            Active = active.Map(value => value.GetStringRequired()),
            Complete = complete.Map(value => value.GetStringRequired())
        };
    }
}
