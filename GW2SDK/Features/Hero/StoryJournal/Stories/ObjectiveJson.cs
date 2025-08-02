using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class ObjectiveJson
{
    public static Objective GetObjective(this in JsonElement json)
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Objective
        {
            Active = active.Map(static (in JsonElement value) => value.GetStringRequired()),
            Complete = complete.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}
