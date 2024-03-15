using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

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
            if (id.Match(member))
            {
                id = member;
            }
            else if (spent.Match(member))
            {
                spent = member;
            }
            else if (done.Match(member))
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
