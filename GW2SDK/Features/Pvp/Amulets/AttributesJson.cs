using System.Text.Json;
using GuildWars2.Hero;

namespace GuildWars2.Pvp.Amulets;

internal static class AttributesJson
{
    public static Dictionary<CombatAttribute, int> GetAttributes(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var result = new Dictionary<CombatAttribute, int>(4);
        foreach (var member in json.EnumerateObject())
        {
            if (Enum.TryParse(member.Name, false, out CombatAttribute name))
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
