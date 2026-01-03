using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class WvwAbilityJson
{
    public static WvwAbility GetWvwAbility(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember rank = "rank";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (rank.Match(member))
            {
                rank = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new WvwAbility
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Rank = rank.Map(static (in value) => value.GetInt32())
        };
    }
}
