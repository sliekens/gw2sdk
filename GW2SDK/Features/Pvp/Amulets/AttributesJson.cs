using System.Text.Json;

namespace GuildWars2.Pvp.Amulets;

internal static class AttributesJson
{
    public static Dictionary<AttributeAdjustmentTarget, int> GetAttributes(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var result = new Dictionary<AttributeAdjustmentTarget, int>(4);
        foreach (var member in json.EnumerateObject())
        {
            if (Enum.TryParse(member.Name, false, out AttributeAdjustmentTarget name))
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
