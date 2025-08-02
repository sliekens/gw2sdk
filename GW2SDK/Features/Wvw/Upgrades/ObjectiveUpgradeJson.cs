using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

internal static class ObjectiveUpgradeJson
{
    public static ObjectiveUpgrade GetObjectiveUpgrade(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember tiers = "tiers";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (tiers.Match(member))
            {
                tiers = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new ObjectiveUpgrade
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Tiers = tiers.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetUpgradeTier())
            )
        };
    }
}
