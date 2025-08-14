using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

internal static class UpgradeTierJson
{
    public static UpgradeTier GetUpgradeTier(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember yaksRequired = "yaks_required";
        RequiredMember upgrades = "upgrades";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (yaksRequired.Match(member))
            {
                yaksRequired = member;
            }
            else if (upgrades.Match(member))
            {
                upgrades = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new UpgradeTier
        {
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            YaksRequired = yaksRequired.Map(static (in JsonElement value) => value.GetInt32()),
            Upgrades = upgrades.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetUpgrade())
            )
        };
    }
}
