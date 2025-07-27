using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Races;

internal static class RaceJson
{
    public static Race GetRace(this in JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Race
        {
            Id = id.Map(static (in JsonElement value) => value.GetEnum<RaceName>()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            SkillIds = skills.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32()))
        };
    }
}
