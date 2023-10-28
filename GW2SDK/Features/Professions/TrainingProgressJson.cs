using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

internal static class TrainingProgressJson
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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == spent.Name)
            {
                spent = member;
            }
            else if (member.Name == done.Name)
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
