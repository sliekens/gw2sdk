using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Exploration.Adventures;

internal static class AdventureJson
{
    public static Adventure GetAdventure(this in JsonElement json)
    {
        RequiredMember coordinates = "coord";
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        foreach (JsonProperty member in json.EnumerateObject())
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
            Id = id.Map(static (in value) => value.GetStringRequired()),
            Coordinates = coordinates.Map(static (in value) => value.GetCoordinateF()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetStringRequired())
        };
    }
}
