using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives;

internal static class SpecialObjectivesProgressJson
{
    public static SpecialObjectivesProgress GetSpecialObjectivesProgress(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember objectives = "objectives";

        foreach (var member in json.EnumerateObject())
        {
            if (objectives.Match(member))
            {
                objectives = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SpecialObjectivesProgress
        {
            Objectives = objectives.Map(
                values => values.GetList(
                    value => value.GetObjectiveProgress(missingMemberBehavior)
                )
            )
        };
    }
}
