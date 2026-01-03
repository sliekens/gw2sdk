using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pve.Dungeons;

internal static class DungeonPathJson
{
    public static DungeonPath GetDungeonPath(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember kind = "type";

        foreach (JsonProperty member in json.EnumerateObject())
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
            Id = id.Map(static (in value) => value.GetStringRequired()),
            Kind = kind.Map(static (in value) => value.GetEnum<DungeonKind>())
        };
    }
}
