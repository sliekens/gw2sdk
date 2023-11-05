using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class PetSkillBarJson
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
            if (member.Name == terrestrial.Name)
            {
                terrestrial = member;
            }
            else if (member.Name == aquatic.Name)
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
            SkillIds =
                terrestrial.Map(values => values.GetList(value => value.GetNullableInt32())),
            AquaticSkillIds =
                aquatic.Map(values => values.GetList(value => value.GetNullableInt32()))
        };
    }
}
