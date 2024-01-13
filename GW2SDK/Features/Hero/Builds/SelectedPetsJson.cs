using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SelectedPetsJson
{
    public static SelectedPets GetSelectedPets(
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

        var (terrestrial1, terrestrial2) = terrestrial.Map(values => values.GetPetIds(missingMemberBehavior));
        var (aquatic1, aquatic2) = aquatic.Map(values => values.GetPetIds(missingMemberBehavior));
        return new SelectedPets
        {
            Terrestrial1 = terrestrial1,
            Terrestrial2 = terrestrial2,
            Aquatic1 = terrestrial1,
            Aquatic2 = terrestrial2
        };
    }
}
