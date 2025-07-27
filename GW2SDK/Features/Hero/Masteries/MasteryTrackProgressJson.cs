using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryTrackProgressJson
{
    public static MasteryTrackProgress GetMasteryTrackProgress(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember level = "level";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (level.Match(member))
            {
                level = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MasteryTrackProgress
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Level = level.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
