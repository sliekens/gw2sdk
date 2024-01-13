using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SelectedSpecializationJson
{
    public static SelectedSpecialization? GetSelectedSpecialization(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        // The API returns "id": null if no specialization is selected, but treat it as required anyway
        RequiredMember id = "id";
        RequiredMember traits = "traits";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                // The API returns { "id": null } if no specialization is selected
                // It's more clear to make the entire object null
                if (member.Value.ValueKind == JsonValueKind.Null)
                {
                    return null;
                }


                id = member;
            }
            else if (member.Name == traits.Name)
            {
                traits = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        var (TraitId1, TraitId2, TraitId3) = traits.Map(values => values.GetTraitIds(missingMemberBehavior));
        return new SelectedSpecialization
        {
            Id = id.Map(value => value.GetInt32()),
            TraitId1 = TraitId1,
            TraitId2 = TraitId2,
            TraitId3 = TraitId3
        };
    }
}
