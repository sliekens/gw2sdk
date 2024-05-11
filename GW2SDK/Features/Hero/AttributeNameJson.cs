using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero;

internal static class AttributeNameJson
{
    public static Extensible<AttributeName> GetAttributeName(this JsonElement json)
    {
        // Somemetimes the old attribute names (or partial names) are used in the API
        if (json.ValueEquals("BoonDuration"))
        {
            return AttributeName.Concentration;
        }

        if (json.ValueEquals("CritDamage"))
        {
            return AttributeName.Ferocity;
        }

        if (json.ValueEquals("ConditionDuration"))
        {
            return AttributeName.Expertise;
        }

        if (json.ValueEquals("Healing"))
        {
            return AttributeName.HealingPower;
        }

        return json.GetEnum<AttributeName>();
    }
}
