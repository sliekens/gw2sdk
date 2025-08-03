using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives;

internal static class ObjectiveJson
{
    public static Objective GetObjective(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember title = "title";
        RequiredMember track = "track";
        RequiredMember acclaim = "acclaim";

        foreach (JsonProperty member in json.EnumerateObject())
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
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Title = title.Map(static (in JsonElement value) => value.GetStringRequired()),
            Track = track.Map(static (in JsonElement value) => value.GetEnum<ObjectiveTrack>()),
            RewardAcclaim = acclaim.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
