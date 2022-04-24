using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats;

[PublicAPI]
public static class SelectedStatReader
{
    public static SelectedStat GetSelectedStat(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SelectedModification> attributes = new("attributes");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(attributes.Name))
            {
                attributes = attributes.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SelectedStat
        {
            Id = id.GetValue(),
            Attributes =
                attributes.Select(value => ReadSelectedModification(value, missingMemberBehavior))
        };
    }

    private static SelectedModification ReadSelectedModification(
        JsonElement json,
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
                agonyResistance = agonyResistance.From(member.Value);
            }
            else if (member.NameEquals(boonDuration.Name))
            {
                boonDuration = boonDuration.From(member.Value);
            }
            else if (member.NameEquals(conditionDamage.Name))
            {
                conditionDamage = conditionDamage.From(member.Value);
            }
            else if (member.NameEquals(conditionDuration.Name))
            {
                conditionDuration = conditionDuration.From(member.Value);
            }
            else if (member.NameEquals(critDamage.Name))
            {
                critDamage = critDamage.From(member.Value);
            }
            else if (member.NameEquals(healing.Name))
            {
                healing = healing.From(member.Value);
            }
            else if (member.NameEquals(power.Name))
            {
                power = power.From(member.Value);
            }
            else if (member.NameEquals(precision.Name))
            {
                precision = precision.From(member.Value);
            }
            else if (member.NameEquals(toughness.Name))
            {
                toughness = toughness.From(member.Value);
            }
            else if (member.NameEquals(vitality.Name))
            {
                vitality = vitality.From(member.Value);
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
