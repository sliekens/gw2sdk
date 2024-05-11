using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class ObjectiveJson
{
    public static Objective GetObjective(this JsonElement json)
    {
        RequiredMember active = "active";
        RequiredMember complete = "complete";

        foreach (var member in json.EnumerateObject())
        {
            if (active.Match(member))
            {
                active = member;
            }
            else if (complete.Match(member))
            {
                complete = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Objective
        {
            Active = active.Map(static value => value.GetStringRequired()),
            Complete = complete.Map(static value => value.GetStringRequired())
        };
    }
}
