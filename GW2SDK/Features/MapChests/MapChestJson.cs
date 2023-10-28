using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.MapChests;

internal static class MapChestJson
{
    public static MapChest GetMapChest(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MapChest { Id = id.Map(value => value.GetStringRequired()) };
    }
}
