using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Professions;

[PublicAPI]
public static class TrainingJson
{
    public static Training GetTraining(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<TrainingCategory> category = new("category");
        RequiredMember<string> name = new("name");
        RequiredMember<TrainingObjective> track = new("track");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(category.Name))
            {
                category.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(track.Name))
            {
                track.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Training
        {
            Id = id.GetValue(),
            Category = category.GetValue(missingMemberBehavior),
            Name = name.GetValue(),
            Track = track.SelectMany(value => value.GetTrainingObjective(missingMemberBehavior))
        };
    }
}
