using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Decorations;

internal static class UnlockedDecorationJson
{
    public static UnlockedDecoration GetUnlockedDecoration(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember count = "count";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
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

        return new UnlockedDecoration
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Count = count.Map(static (in value) => value.GetInt32())
        };
    }
}
