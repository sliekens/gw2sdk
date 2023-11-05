using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Professions;

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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == category.Name)
            {
                category = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == track.Name)
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
                category.Map(value => value.GetEnum<TrainingCategory>(missingMemberBehavior)),
            Name = name.Map(value => value.GetStringRequired()),
            Track = track.Map(
                values => values.GetList(value => value.GetTrainingObjective(missingMemberBehavior))
            )
        };
    }
}
