using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Dungeons;

internal static class DungeonPathJson
{
    public static DungeonPath GetDungeonPath(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember kind = "type";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (kind.Match(member))
            {
                kind = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new DungeonPath
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Kind = kind.Map(static value => value.GetEnum<DungeonKind>())
        };
    }
}
