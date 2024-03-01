using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives;

internal static class ObjectiveProgressJson
{
    public static ObjectiveProgress GetObjectiveProgress(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
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
            else if (member.Name == progressCurrent.Name)
            {
                progressCurrent = member;
            }
            else if (member.Name == progressComplete.Name)
            {
                progressComplete = member;
            }
            else if (member.Name == claimed.Name)
            {
                claimed = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ObjectiveProgress
        {
            Id = id.Map(value => value.GetInt32()),
            Title = title.Map(value => value.GetStringRequired()),
            Track = track.Map(value => value.GetEnum<ObjectiveTrack>(missingMemberBehavior)),
            RewardAcclaim = acclaim.Map(value => value.GetInt32()),
            Progress = progressCurrent.Map(value => value.GetInt32()),
            Goal = progressComplete.Map(value => value.GetInt32()),
            Claimed = claimed.Map(value => value.GetBoolean())
        };
    }
}
