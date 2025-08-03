using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SelectedPetsJson
{
    public static SelectedPets GetSelectedPets(this in JsonElement json)
    {
        RequiredMember terrestrial = "terrestrial";
        RequiredMember aquatic = "aquatic";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (terrestrial.Match(member))
            {
                terrestrial = member;
            }
            else if (aquatic.Match(member))
            {
                aquatic = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        (int? terrestrial1, int? terrestrial2) = terrestrial.Map(static (in JsonElement values) => values.GetPetIds());
        (int? aquatic1, int? aquatic2) = aquatic.Map(static (in JsonElement values) => values.GetPetIds());
        return new SelectedPets
        {
            Terrestrial1 = terrestrial1,
            Terrestrial2 = terrestrial2,
            Aquatic1 = aquatic1,
            Aquatic2 = aquatic2
        };
    }
}
