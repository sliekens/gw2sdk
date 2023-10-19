using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.ItemStats;

[PublicAPI]
public static class SelectedModificationJson
{
    public static SelectedModification GetSelectedModification(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember agonyResistance = new("AgonyResistance");
        NullableMember boonDuration = new("BoonDuration");
        NullableMember conditionDamage = new("ConditionDamage");
        NullableMember conditionDuration = new("ConditionDuration");
        NullableMember critDamage = new("CritDamage");
        NullableMember healing = new("Healing");
        NullableMember power = new("Power");
        NullableMember precision = new("Precision");
        NullableMember toughness = new("Toughness");
        NullableMember vitality = new("Vitality");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(agonyResistance.Name))
            {
                agonyResistance = member;
            }
            else if (member.NameEquals(boonDuration.Name))
            {
                boonDuration = member;
            }
            else if (member.NameEquals(conditionDamage.Name))
            {
                conditionDamage = member;
            }
            else if (member.NameEquals(conditionDuration.Name))
            {
                conditionDuration = member;
            }
            else if (member.NameEquals(critDamage.Name))
            {
                critDamage = member;
            }
            else if (member.NameEquals(healing.Name))
            {
                healing = member;
            }
            else if (member.NameEquals(power.Name))
            {
                power = member;
            }
            else if (member.NameEquals(precision.Name))
            {
                precision = member;
            }
            else if (member.NameEquals(toughness.Name))
            {
                toughness = member;
            }
            else if (member.NameEquals(vitality.Name))
            {
                vitality = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SelectedModification
        {
            AgonyResistance = agonyResistance.Select(value => value.GetInt32()),
            BoonDuration = boonDuration.Select(value => value.GetInt32()),
            ConditionDamage = conditionDamage.Select(value => value.GetInt32()),
            ConditionDuration = conditionDuration.Select(value => value.GetInt32()),
            CritDamage = critDamage.Select(value => value.GetInt32()),
            Healing = healing.Select(value => value.GetInt32()),
            Power = power.Select(value => value.GetInt32()),
            Precision = precision.Select(value => value.GetInt32()),
            Toughness = toughness.Select(value => value.GetInt32()),
            Vitality = vitality.Select(value => value.GetInt32())
        };
    }
}
