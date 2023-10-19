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
        NullableMember agonyResistance = "AgonyResistance";
        NullableMember boonDuration = "BoonDuration";
        NullableMember conditionDamage = "ConditionDamage";
        NullableMember conditionDuration = "ConditionDuration";
        NullableMember critDamage = "CritDamage";
        NullableMember healing = "Healing";
        NullableMember power = "Power";
        NullableMember precision = "Precision";
        NullableMember toughness = "Toughness";
        NullableMember vitality = "Vitality";
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
            AgonyResistance = agonyResistance.Map(value => value.GetInt32()),
            BoonDuration = boonDuration.Map(value => value.GetInt32()),
            ConditionDamage = conditionDamage.Map(value => value.GetInt32()),
            ConditionDuration = conditionDuration.Map(value => value.GetInt32()),
            CritDamage = critDamage.Map(value => value.GetInt32()),
            Healing = healing.Map(value => value.GetInt32()),
            Power = power.Map(value => value.GetInt32()),
            Precision = precision.Map(value => value.GetInt32()),
            Toughness = toughness.Map(value => value.GetInt32()),
            Vitality = vitality.Map(value => value.GetInt32())
        };
    }
}
