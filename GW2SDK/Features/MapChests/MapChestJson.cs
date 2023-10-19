using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.MapChests;

[PublicAPI]
public static class MapChestJson
{
    public static MapChest GetMapChest(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MapChest { Id = id.Select(value => value.GetStringRequired()) };
    }
}
