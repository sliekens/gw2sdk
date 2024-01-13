using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SelectedSpecializationJson
{
    public static SelectedSpecialization GetSelectedSpecialization(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember id = "id";
        RequiredMember traits = "traits";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
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

        var traitIds = traits.Map(values => values.GetTraitIds(missingMemberBehavior));
        return new SelectedSpecialization
        {
            Id = id.Map(value => value.GetInt32()),
            TraitId1 = traitIds.TraitId,
            TraitId2 = traitIds.TraitId2,
            TraitId3 = traitIds.TraitId3
        };
    }
}
