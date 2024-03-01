using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives;

internal static class WeeklyObjectivesProgressJson
{
    public static WeeklyObjectivesProgress GetWeeklyObjectivesProgress(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember metaProgressCurrent = "meta_progress_current";
        RequiredMember metaProgressComplete = "meta_progress_complete";
        RequiredMember metaRewardItemId = "meta_reward_item_id";
        RequiredMember metaRewardAstral = "meta_reward_astral";
        RequiredMember metaRewardClaimed = "meta_reward_claimed";
        RequiredMember objectives = "objectives";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == metaProgressCurrent.Name)
            {
                metaProgressCurrent = member;
            }
            else if (member.Name == metaProgressComplete.Name)
            {
                metaProgressComplete = member;
            }
            else if (member.Name == metaRewardItemId.Name)
            {
                metaRewardItemId = member;
            }
            else if (member.Name == metaRewardAstral.Name)
            {
                metaRewardAstral = member;
            }
            else if (member.Name == metaRewardClaimed.Name)
            {
                metaRewardClaimed = member;
            }
            else if (member.Name == objectives.Name)
            {
                objectives = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeeklyObjectivesProgress
        {
            Progress = metaProgressCurrent.Map(value => value.GetInt32()),
            Goal = metaProgressComplete.Map(value => value.GetInt32()),
            RewardItemId = metaRewardItemId.Map(value => value.GetInt32()),
            RewardAcclaim = metaRewardAstral.Map(value => value.GetInt32()),
            Claimed = metaRewardClaimed.Map(value => value.GetBoolean()),
            Objectives = objectives.Map(
                values => values.GetList(
                    value => value.GetObjectiveProgress(missingMemberBehavior)
                )
            )
        };
    }
}
