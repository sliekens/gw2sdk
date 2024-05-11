using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Sectors;

internal static class SectorJson
{
    public static Sector GetSector(this JsonElement json)
    {
        OptionalMember name = "name";
        RequiredMember level = "level";
        RequiredMember coordinates = "coord";
        RequiredMember boundaries = "bounds";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        foreach (var member in json.EnumerateObject())
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Sector
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetString()) ?? "",
            Level = level.Map(static value => value.GetInt32()),
            Coordinates = coordinates.Map(static value => value.GetCoordinateF()),
            Boundaries =
                boundaries.Map(
                    static values => values.GetList(static value => value.GetCoordinateF())
                ),
            ChatLink = chatLink.Map(static value => value.GetStringRequired())
        };
    }
}
