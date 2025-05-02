using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Metadata;

internal static class ApiVersionJson
{
    public static ApiVersion GetApiVersion(this JsonElement json)
    {
        RequiredMember languages = "langs";
        RequiredMember routes = "routes";
        OptionalMember schemaVersions = "schema_versions";

        foreach (var member in json.EnumerateObject())
        {
            if (languages.Match(member))
            {
                languages = member;
            }
            else if (routes.Match(member))
            {
                routes = member;
            }
            else if (schemaVersions.Match(member))
            {
                schemaVersions = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new ApiVersion
        {
            Languages =
                languages.Map(static values =>
                    values.GetList(static value => value.GetStringRequired())
                ),
            Routes =
                routes.Map(static values => values.GetList(static value => value.GetRoute())),
            SchemaVersions = schemaVersions.Map(static values =>
                    values.GetList(static value => value.GetSchema())
                )
                ?? []
        };
    }
}
