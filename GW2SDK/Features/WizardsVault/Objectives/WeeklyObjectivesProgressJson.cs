using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives;

internal static class WeeklyObjectivesProgressJson
{
    public static WeeklyObjectivesProgress GetWeeklyObjectivesProgress(this JsonElement json)
    {
        RequiredMember metaProgressCurrent = "meta_progress_current";
        RequiredMember metaProgressComplete = "meta_progress_complete";
        RequiredMember metaRewardItemId = "meta_reward_item_id";
        RequiredMember metaRewardAstral = "meta_reward_astral";
        RequiredMember metaRewardClaimed = "meta_reward_claimed";
        RequiredMember objectives = "objectives";

        foreach (var member in json.EnumerateObject())
        {
            if (metaProgressCurrent.Match(member))
            {
                metaProgressCurrent = member;
            }
            else if (metaProgressComplete.Match(member))
            {
                metaProgressComplete = member;
            }
            else if (metaRewardItemId.Match(member))
            {
                metaRewardItemId = member;
            }
            else if (metaRewardAstral.Match(member))
            {
                metaRewardAstral = member;
            }
            else if (metaRewardClaimed.Match(member))
            {
                metaRewardClaimed = member;
            }
            else if (objectives.Match(member))
            {
                objectives = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new WeeklyObjectivesProgress
        {
            Progress = metaProgressCurrent.Map(static value => value.GetInt32()),
            Goal = metaProgressComplete.Map(static value => value.GetInt32()),
            RewardItemId = metaRewardItemId.Map(static value => value.GetInt32()),
            RewardAcclaim = metaRewardAstral.Map(static value => value.GetInt32()),
            Claimed = metaRewardClaimed.Map(static value => value.GetBoolean()),
            Objectives = objectives.Map(
                static values => values.GetList(static value => value.GetObjectiveProgress())
            )
        };
    }
}
