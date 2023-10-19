using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class TrainingProgressJson
{
    public static TrainingProgress GetTrainingProgress(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember spent = "spent";
        RequiredMember done = "done";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(spent.Name))
            {
                spent = member;
            }
            else if (member.NameEquals(done.Name))
            {
                done = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TrainingProgress
        {
            Id = id.Map(value => value.GetInt32()),
            Spent = spent.Map(value => value.GetInt32()),
            Done = done.Map(value => value.GetBoolean())
        };
    }
}
