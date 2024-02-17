using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment;

internal static class AttributesJson
{
    public static Dictionary<AttributeName, int> GetAttributes(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var result = new Dictionary<AttributeName, int>(4);
        foreach (var member in json.EnumerateObject())
        {
            var value = member.Value.GetInt32();

            // Somemetimes the old attribute names (or partial names) are used in the API
            if (member.NameEquals("BoonDuration"))
            {
                result[AttributeName.Concentration] = value;
            }
            else if (member.NameEquals("CritDamage"))
            {
                result[AttributeName.Ferocity] = value;
            }
            else if (member.NameEquals("ConditionDuration"))
            {
                result[AttributeName.Expertise] = value;
            }
            else if (member.NameEquals("Healing"))
            {
                result[AttributeName.HealingPower] = value;
            }
            else
            {
                result[json.GetEnum<AttributeName>(missingMemberBehavior)] = value;
            }
        }

        return result;
    }
}
