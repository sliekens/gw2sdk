using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Exploration.Sectors;

internal static class SectorJson
{
    public static Sector GetSector(this in JsonElement json)
    {
        OptionalMember name = "name";
        RequiredMember level = "level";
        RequiredMember coordinates = "coord";
        RequiredMember boundaries = "bounds";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (level.Match(member))
            {
                level = member;
            }
            else if (coordinates.Match(member))
            {
                coordinates = member;
            }
            else if (boundaries.Match(member))
            {
                boundaries = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Sector
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetString()) ?? "",
            Level = level.Map(static (in value) => value.GetInt32()),
            Coordinates = coordinates.Map(static (in value) => value.GetCoordinateF()),
            Boundaries =
                boundaries.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetCoordinateF())
                ),
            ChatLink = chatLink.Map(static (in value) => value.GetStringRequired())
        };
    }
}
