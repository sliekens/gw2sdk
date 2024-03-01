using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives;

internal static class ObjectiveJson
{
    public static Objective GetObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember title = "title";
        RequiredMember track = "track";
        RequiredMember acclaim = "acclaim";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == title.Name)
            {
                title = member;
            }
            else if (member.Name == track.Name)
            {
                track = member;
            }
            else if (member.Name == acclaim.Name)
            {
                acclaim = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Objective
        {
            Id = id.Map(value => value.GetInt32()),
            Title = title.Map(value => value.GetStringRequired()),
            Track = track.Map(value => value.GetEnum<ObjectiveTrack>(missingMemberBehavior)),
            Acclaim = acclaim.Map(value => value.GetInt32())
        };
    }
}
