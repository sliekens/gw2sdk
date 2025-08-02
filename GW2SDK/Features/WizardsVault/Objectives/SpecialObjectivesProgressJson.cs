using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives;

internal static class SpecialObjectivesProgressJson
{
    public static SpecialObjectivesProgress GetSpecialObjectivesProgress(this in JsonElement json)
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new SpecialObjectivesProgress
        {
            Objectives = objectives.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetObjectiveProgress())
            )
        };
    }
}
