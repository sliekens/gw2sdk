using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives;

internal static class SpecialObjectivesProgressJson
{
    public static SpecialObjectivesProgress GetSpecialObjectivesProgress(this JsonElement json)
    {
        RequiredMember objectives = "objectives";

        foreach (var member in json.EnumerateObject())
        {
            if (objectives.Match(member))
            {
                objectives = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SpecialObjectivesProgress
        {
            Objectives = objectives.Map(
                static values => values.GetList(static value => value.GetObjectiveProgress())
            )
        };
    }
}
