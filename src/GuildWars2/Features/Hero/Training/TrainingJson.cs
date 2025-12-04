using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class TrainingJson
{
    public static Training GetTraining(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember category = "category";
        RequiredMember name = "name";
        RequiredMember track = "track";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Training
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Category = category.Map(static (in value) => value.GetEnum<TrainingCategory>()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Track = track.Map(static (in values) =>
                values.GetList(static (in value) => value.GetTrainingObjective())
            )
        };
    }
}
