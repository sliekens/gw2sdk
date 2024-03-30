using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class TrainingJson
{
    public static Training GetTraining(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember category = "category";
        RequiredMember name = "name";
        RequiredMember track = "track";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (category.Match(member))
            {
                category = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (track.Match(member))
            {
                track = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Training
        {
            Id = id.Map(value => value.GetInt32()),
            Category =
                category.Map(value => value.GetEnum<TrainingCategory>()),
            Name = name.Map(value => value.GetStringRequired()),
            Track = track.Map(
                values => values.GetList(value => value.GetTrainingObjective(missingMemberBehavior))
            )
        };
    }
}
