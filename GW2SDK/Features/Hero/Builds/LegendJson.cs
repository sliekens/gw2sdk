using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class LegendJson
{
    public static Legend GetLegend(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember code = "code";
        RequiredMember swap = "swap";
        RequiredMember heal = "heal";
        RequiredMember elite = "elite";
        RequiredMember utilities = "utilities";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (swap.Match(member))
            {
                swap = member;
            }
            else if (code.Match(member))
            {
                code = member;
            }
            else if (heal.Match(member))
            {
                heal = member;
            }
            else if (elite.Match(member))
            {
                elite = member;
            }
            else if (utilities.Match(member))
            {
                utilities = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Legend
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Code = code.Map(static (in JsonElement value) => value.GetInt32()),
            Swap = swap.Map(static (in JsonElement value) => value.GetInt32()),
            Heal = heal.Map(static (in JsonElement value) => value.GetInt32()),
            Elite = elite.Map(static (in JsonElement value) => value.GetInt32()),
            Utilities = utilities.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetInt32())
            )
        };
    }
}
