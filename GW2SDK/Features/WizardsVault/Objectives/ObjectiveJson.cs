using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives;

internal static class ObjectiveJson
{
    public static Objective GetObjective(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember title = "title";
        RequiredMember track = "track";
        RequiredMember acclaim = "acclaim";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (title.Match(member))
            {
                title = member;
            }
            else if (track.Match(member))
            {
                track = member;
            }
            else if (acclaim.Match(member))
            {
                acclaim = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Objective
        {
            Id = id.Map(static value => value.GetInt32()),
            Title = title.Map(static value => value.GetStringRequired()),
            Track = track.Map(static value => value.GetEnum<ObjectiveTrack>()),
            RewardAcclaim = acclaim.Map(static value => value.GetInt32())
        };
    }
}
