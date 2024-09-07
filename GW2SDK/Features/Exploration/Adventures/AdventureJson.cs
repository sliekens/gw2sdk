using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Adventures;

internal static class AdventureJson
{
    public static Adventure GetAdventure(this JsonElement json)
    {
        RequiredMember coordinates = "coord";
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        foreach (var member in json.EnumerateObject())
        {
            if (coordinates.Match(member))
            {
                coordinates = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Adventure
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Coordinates = coordinates.Map(static value => value.GetCoordinateF()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetStringRequired())
        };
    }
}
