using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class TrainingJson
{
    public static Training GetTraining(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember category = new("category");
        RequiredMember name = new("name");
        RequiredMember track = new("track");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(category.Name))
            {
                category = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(track.Name))
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
            Id = id.Select(value => value.GetInt32()),
            Category = category.Select(value => value.GetEnum<TrainingCategory>(missingMemberBehavior)),
            Name = name.Select(value => value.GetStringRequired()),
            Track = track.SelectMany(value => value.GetTrainingObjective(missingMemberBehavior))
        };
    }
}
