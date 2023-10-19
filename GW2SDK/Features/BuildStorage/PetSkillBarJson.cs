using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public static class PetSkillBarJson
{
    public static PetSkillBar GetPetSkillBar(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember terrestrial = "terrestrial";
        RequiredMember aquatic = "aquatic";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(terrestrial.Name))
            {
                terrestrial = member;
            }
            else if (member.NameEquals(aquatic.Name))
            {
                aquatic = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PetSkillBar
        {
            Terrestrial =
                terrestrial.Select(values => values.GetList(value => value.GetNullableInt32())),
            Aquatic = aquatic.Select(
                values => values.GetList(value => value.GetNullableInt32())
            )
        };
    }
}
