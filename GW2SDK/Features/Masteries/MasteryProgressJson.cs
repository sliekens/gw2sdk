using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Masteries;

[PublicAPI]
public static class MasteryProgressJson
{
    public static MasteryProgress GetMasteryProgress(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember level = new("level");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryProgress
        {
            Id = id.Select(value => value.GetInt32()),
            Level = level.Select(value => value.GetInt32())
        };
    }
}
