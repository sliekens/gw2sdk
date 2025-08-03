using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pve.MapChests;

internal static class MapChestJson
{
    public static MapChest GetMapChest(this in JsonElement json)
    {
        RequiredMember id = "id";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MapChest { Id = id.Map(static (in JsonElement value) => value.GetStringRequired()) };
    }
}
