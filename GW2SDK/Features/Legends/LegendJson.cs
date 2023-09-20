using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Legends;

[PublicAPI]
public static class LegendJson
{
    public static Legend GetLegend(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<int> code = new("code");
        RequiredMember<int> swap = new("swap");
        RequiredMember<int> heal = new("heal");
        RequiredMember<int> elite = new("elite");
        RequiredMember<int> utilities = new("utilities");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(swap.Name))
            {
                swap.Value = member.Value;
            }
            else if (member.NameEquals(code.Name))
            {
                code.Value = member.Value;
            }
            else if (member.NameEquals(heal.Name))
            {
                heal.Value = member.Value;
            }
            else if (member.NameEquals(elite.Name))
            {
                elite.Value = member.Value;
            }
            else if (member.NameEquals(utilities.Name))
            {
                utilities.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Legend
        {
            Id = id.GetValue(),
            Code = code.GetValue(),
            Swap = swap.GetValue(),
            Heal = heal.GetValue(),
            Elite = elite.GetValue(),
            Utilities = utilities.SelectMany(value => value.GetInt32())
        };
    }
}
