using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Adventures;

[PublicAPI]
public static class AdventureJson
{
    public static Adventure GetAdventure(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember coordinates = new("coord");
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coordinates.Name))
            {
                coordinates.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Adventure
        {
            Id = id.Select(value => value.GetStringRequired()),
            Coordinates = coordinates.Select(value => value.GetCoordinateF(missingMemberBehavior)),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired())
        };
    }
}
