using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SelectedSpecializationJson
{
    public static SelectedSpecialization? GetSelectedSpecialization(this JsonElement json)
    {
        // The API returns "id": null if no specialization is selected, but treat it as required anyway
        RequiredMember id = "id";
        RequiredMember traits = "traits";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                // The API returns { "id": null } if no specialization is selected
                // It's more clear to make the entire object null
                if (member.Value.ValueKind == JsonValueKind.Null)
                {
                    return null;
                }

                id = member;
            }
            else if (traits.Match(member))
            {
                traits = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        var (adept, master, grandmaster) = traits.Map(static values => values.GetTraitIds());
        return new SelectedSpecialization
        {
            Id = id.Map(static value => value.GetInt32()),
            AdeptTraitId = adept,
            MasterTraitId = master,
            GrandmasterTraitId = grandmaster
        };
    }
}
