using System.Text.Json;

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
            // Somemetimes the old attribute names (or partial names) are used in the API
            if (member.NameEquals("BoonDuration"))
            {
                result[AttributeName.Concentration] = member.Value.GetInt32();
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
            else if (Enum.TryParse(member.Name, false, out AttributeName name))
            {
                result[name] = member.Value.GetInt32();
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return result;
    }
}
