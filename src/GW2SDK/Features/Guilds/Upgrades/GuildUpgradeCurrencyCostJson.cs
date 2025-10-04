using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class GuildUpgradeCurrencyCostJson
{
    public static GuildUpgradeCurrencyCost GetGuildUpgradeCurrencyCost(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember count = "count";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Currency"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildUpgradeCurrencyCost
        {
            Name = name.Map(static (in value) => value.GetString()) ?? "",
            Count = count.Map(static (in value) => value.GetInt32())
        };
    }
}
