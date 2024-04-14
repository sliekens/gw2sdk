using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives;

internal static class ObjectiveProgressJson
{
    public static ObjectiveProgress GetObjectiveProgress(
        this JsonElement json
    )
    {
        RequiredMember id = "id";
        RequiredMember title = "title";
        RequiredMember track = "track";
        RequiredMember acclaim = "acclaim";
        RequiredMember progressCurrent = "progress_current";
        RequiredMember progressComplete = "progress_complete";
        RequiredMember claimed = "claimed";

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
            else if (progressCurrent.Match(member))
            {
                progressCurrent = member;
            }
            else if (progressComplete.Match(member))
            {
                progressComplete = member;
            }
            else if (claimed.Match(member))
            {
                claimed = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ObjectiveProgress
        {
            Id = id.Map(static value => value.GetInt32()),
            Title = title.Map(static value => value.GetStringRequired()),
            Track = track.Map(static value => value.GetEnum<ObjectiveTrack>()),
            RewardAcclaim = acclaim.Map(static value => value.GetInt32()),
            Progress = progressCurrent.Map(static value => value.GetInt32()),
            Goal = progressComplete.Map(static value => value.GetInt32()),
            Claimed = claimed.Map(static value => value.GetBoolean())
        };
    }
}
