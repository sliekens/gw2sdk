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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == swap.Name)
            {
                swap = member;
            }
            else if (member.Name == code.Name)
            {
                code = member;
            }
            else if (member.Name == heal.Name)
            {
                heal = member;
            }
            else if (member.Name == elite.Name)
            {
                elite = member;
            }
            else if (member.Name == utilities.Name)
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
