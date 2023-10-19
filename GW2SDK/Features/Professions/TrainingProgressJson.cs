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
        RequiredMember id = new("id");
        RequiredMember spent = new("spent");
        RequiredMember done = new("done");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(spent.Name))
            {
                spent.Value = member.Value;
            }
            else if (member.NameEquals(done.Name))
            {
                done.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TrainingProgress
        {
            Id = id.Select(value => value.GetInt32()),
            Spent = spent.Select(value => value.GetInt32()),
            Done = done.Select(value => value.GetBoolean())
        };
    }
}
