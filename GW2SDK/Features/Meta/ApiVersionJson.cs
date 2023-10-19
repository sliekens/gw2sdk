using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Meta;

[PublicAPI]
public static class ApiVersionJson
{
    public static ApiVersion GetApiVersion(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember languages = "langs";
        RequiredMember routes = "routes";
        OptionalMember schemaVersions = "schema_versions";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(languages.Name))
            {
                languages = member;
            }
            else if (member.NameEquals(routes.Name))
            {
                routes = member;
            }
            else if (member.NameEquals(schemaVersions.Name))
            {
                schemaVersions = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ApiVersion
        {
            Languages = languages.Map(values => values.GetList(value => value.GetStringRequired())),
            Routes = routes.Map(values => values.GetList(value => value.GetRoute(missingMemberBehavior))),
            SchemaVersions =
                schemaVersions.Map(values => values.GetList(value => value.GetSchema(missingMemberBehavior)))
                ?? new List<Schema>()
        };
    }
}
