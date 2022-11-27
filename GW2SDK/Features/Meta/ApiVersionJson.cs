using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Meta;

[PublicAPI]
public static class ApiVersionJson
{
    public static ApiVersion GetApiVersion(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> languages = new("langs");
        RequiredMember<Route> routes = new("routes");
        OptionalMember<Schema> schemaVersions = new("schema_versions");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(languages.Name))
            {
                languages.Value = member.Value;
            }
            else if (member.NameEquals(routes.Name))
            {
                routes.Value = member.Value;
            }
            else if (member.NameEquals(schemaVersions.Name))
            {
                schemaVersions.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ApiVersion
        {
            Languages = languages.SelectMany(value => value.GetStringRequired()),
            Routes = routes.SelectMany(value => value.GetRoute(missingMemberBehavior)),
            SchemaVersions =
                schemaVersions.SelectMany(value => value.GetSchema(missingMemberBehavior))
                ?? Array.Empty<Schema>()
        };
    }
}
