using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryTrackProgressJson
{
    public static MasteryTrackProgress GetMasteryTrackProgress(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember level = "level";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == level.Name)
            {
                level = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryTrackProgress
        {
            Id = id.Map(value => value.GetInt32()),
            Level = level.Map(value => value.GetInt32())
        };
    }
}
