using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Dungeons;

internal static class DungeonJson
{
    public static Dungeon GetDungeon(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember paths = "paths";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (paths.Match(member))
            {
                paths = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Dungeon
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Paths = paths.Map(static values =>
                values.GetList(static value => value.GetDungeonPath())
            )
        };
    }
}
