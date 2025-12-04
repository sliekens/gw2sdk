using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class TrainingProgressJson
{
    public static TrainingProgress GetTrainingProgress(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember spent = "spent";
        RequiredMember done = "done";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new TrainingProgress
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Spent = spent.Map(static (in value) => value.GetInt32()),
            Done = done.Map(static (in value) => value.GetBoolean())
        };
    }
}
