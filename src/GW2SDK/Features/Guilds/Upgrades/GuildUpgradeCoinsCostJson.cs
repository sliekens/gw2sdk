using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class GuildUpgradeCoinsCostJson
{
    public static GuildUpgradeCoinsCost GetGuildUpgradeCoinsCost(this in JsonElement json)
    {
        RequiredMember count = "count";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Coins"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
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

        return new GuildUpgradeCoinsCost { Coins = count.Map(static (in value) => value.GetInt32()) };
    }
}
