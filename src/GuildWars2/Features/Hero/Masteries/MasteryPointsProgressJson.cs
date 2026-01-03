using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryPointsProgressJson
{
    public static MasteryPointsProgress GetMasteryPointsProgress(this in JsonElement json)
    {
        RequiredMember totals = "totals";
        RequiredMember unlocked = "unlocked";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (totals.Match(member))
            {
                totals = member;
            }
            else if (unlocked.Match(member))
            {
                unlocked = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MasteryPointsProgress
        {
            Totals =
                totals.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetMasteryPointsTotal())
                ),
            Unlocked = unlocked.Map(static (in values) =>
                values.GetList(static (in value) => value.GetInt32())
            )
        };
    }
}
