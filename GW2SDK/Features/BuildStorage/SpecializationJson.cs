using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.BuildStorage;

internal static class SpecializationJson
{
    public static Specialization GetSpecialization(
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

        return new Specialization
        {
            Id = id.Map(value => value.GetInt32()),
            TraitIds = traits.Map(values => values.GetList(value => value.GetNullableInt32()))
        };
    }
}
