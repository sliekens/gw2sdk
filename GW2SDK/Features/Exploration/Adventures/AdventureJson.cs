using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Adventures;

internal static class AdventureJson
{
    public static Adventure GetAdventure(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Adventure
        {
            Id = id.Map(value => value.GetStringRequired()),
            Coordinates = coordinates.Map(value => value.GetCoordinateF(missingMemberBehavior)),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired())
        };
    }
}
