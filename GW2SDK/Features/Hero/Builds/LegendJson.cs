using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class LegendJson
{
    public static Legend GetLegend(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Legend
        {
            Id = id.Map(value => value.GetStringRequired()),
            Code = code.Map(value => value.GetInt32()),
            Swap = swap.Map(value => value.GetInt32()),
            Heal = heal.Map(value => value.GetInt32()),
            Elite = elite.Map(value => value.GetInt32()),
            Utilities = utilities.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
