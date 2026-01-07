using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment;

internal static class AttributesJson
{
    public static ValueDictionary<Extensible<AttributeName>, int> GetAttributes(this in JsonElement json)
    {
        ValueDictionary<Extensible<AttributeName>, int> result = new(4);
        foreach (JsonProperty member in json.EnumerateObject())
        {
            // Somemetimes the old attribute names (or partial names) are used in the API
            if (member.NameEquals("Power"))
            {
                result[AttributeName.Power] = member.Value.GetInt32();
            }
            else if (member.NameEquals("Precision"))
            {
                result[AttributeName.Precision] = member.Value.GetInt32();
            }
            else if (member.NameEquals("Toughness"))
            {
                result[AttributeName.Toughness] = member.Value.GetInt32();
            }
            else if (member.NameEquals("Vitality"))
            {
                result[AttributeName.Vitality] = member.Value.GetInt32();
            }
            else if (member.NameEquals("BoonDuration"))
            {
                result[AttributeName.Concentration] = member.Value.GetInt32();
            }
            else if (member.NameEquals("ConditionDamage"))
            {
                result[AttributeName.ConditionDamage] = member.Value.GetInt32();
            }
            else if (member.NameEquals("CritDamage"))
            {
                result[AttributeName.Ferocity] = member.Value.GetInt32();
            }
            else if (member.NameEquals("ConditionDuration"))
            {
                result[AttributeName.Expertise] = member.Value.GetInt32();
            }
            else if (member.NameEquals("Healing"))
            {
                result[AttributeName.HealingPower] = member.Value.GetInt32();
            }
            else if (member.NameEquals("AgonyResistance"))
            {
                result[AttributeName.AgonyResistance] = member.Value.GetInt32();
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
            else
            {
                result[member.Name] = member.Value.GetInt32();
            }
        }

        return result;
    }
}
