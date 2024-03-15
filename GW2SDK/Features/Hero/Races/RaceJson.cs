using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Races;

internal static class RaceJson
{
    public static Race GetRace(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember skills = "skills";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (skills.Match(member))
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
            Id = id.Map(value => value.GetEnum<RaceName>(missingMemberBehavior)),
            Name = name.Map(value => value.GetStringRequired()),
            SkillIds = skills.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
