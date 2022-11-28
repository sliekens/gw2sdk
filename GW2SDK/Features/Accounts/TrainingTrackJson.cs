using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Accounts;

[PublicAPI]
public static class TrainingTrackJson
{
    public static TrainingTrack GetTrainingTrack(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> spent = new("spent");
        RequiredMember<bool> done = new("done");

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

        return new TrainingTrack
        {
            Id = id.GetValue(),
            Spent = spent.GetValue(),
            Done = done.GetValue()
        };
    }
}
