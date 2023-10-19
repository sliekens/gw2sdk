using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Races;

[PublicAPI]
public static class RaceJson
{
    public static Race GetRace(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember skills = "skills";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Race
        {
            Id = id.Select(value => value.GetEnum<RaceName>(missingMemberBehavior)),
            Name = name.Select(value => value.GetStringRequired()),
            Skills = skills.SelectMany(value => value.GetInt32())
        };
    }
}
