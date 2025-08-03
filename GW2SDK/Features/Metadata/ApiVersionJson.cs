using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Metadata;

internal static class ApiVersionJson
{
    public static ApiVersion GetApiVersion(this in JsonElement json)
    {
        RequiredMember languages = "langs";
        RequiredMember routes = "routes";
        OptionalMember schemaVersions = "schema_versions";

        foreach (JsonProperty member in json.EnumerateObject())
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
                languages.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetStringRequired())
                ),
            Routes =
                routes.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetRoute())),
            SchemaVersions = schemaVersions.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetSchema())
                )
                ?? new Collections.ValueList<Schema>()
        };
    }
}
