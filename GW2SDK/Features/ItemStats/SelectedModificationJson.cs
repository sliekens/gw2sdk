using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.ItemStats;

[PublicAPI]
public static class SelectedModificationJson
{
    public static SelectedModification GetSelectedModification(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember<int> agonyResistance = new("AgonyResistance");
        NullableMember<int> boonDuration = new("BoonDuration");
        NullableMember<int> conditionDamage = new("ConditionDamage");
        NullableMember<int> conditionDuration = new("ConditionDuration");
        NullableMember<int> critDamage = new("CritDamage");
        NullableMember<int> healing = new("Healing");
        NullableMember<int> power = new("Power");
        NullableMember<int> precision = new("Precision");
        NullableMember<int> toughness = new("Toughness");
        NullableMember<int> vitality = new("Vitality");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(agonyResistance.Name))
            {
                agonyResistance.Value = member.Value;
            }
            else if (member.NameEquals(boonDuration.Name))
            {
                boonDuration.Value = member.Value;
            }
            else if (member.NameEquals(conditionDamage.Name))
            {
                conditionDamage.Value = member.Value;
            }
            else if (member.NameEquals(conditionDuration.Name))
            {
                conditionDuration.Value = member.Value;
            }
            else if (member.NameEquals(critDamage.Name))
            {
                critDamage.Value = member.Value;
            }
            else if (member.NameEquals(healing.Name))
            {
                healing.Value = member.Value;
            }
            else if (member.NameEquals(power.Name))
            {
                power.Value = member.Value;
            }
            else if (member.NameEquals(precision.Name))
            {
                precision.Value = member.Value;
            }
            else if (member.NameEquals(toughness.Name))
            {
                toughness.Value = member.Value;
            }
            else if (member.NameEquals(vitality.Name))
            {
                vitality.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SelectedModification
        {
            AgonyResistance = agonyResistance.GetValue(),
            BoonDuration = boonDuration.GetValue(),
            ConditionDamage = conditionDamage.GetValue(),
            ConditionDuration = conditionDuration.GetValue(),
            CritDamage = critDamage.GetValue(),
            Healing = healing.GetValue(),
            Power = power.GetValue(),
            Precision = precision.GetValue(),
            Toughness = toughness.GetValue(),
            Vitality = vitality.GetValue()
        };
    }
}
