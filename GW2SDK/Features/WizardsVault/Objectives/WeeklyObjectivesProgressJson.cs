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
