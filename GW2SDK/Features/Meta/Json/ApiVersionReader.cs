using System;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Meta.Models;
using JetBrains.Annotations;

namespace GW2SDK.Meta.Json;

[PublicAPI]
public static class ApiVersionReader
{
    public static ApiVersion Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> languages = new("langs");
        RequiredMember<Route> routes = new("routes");
        OptionalMember<Schema> schemaVersions = new("schema_versions");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(languages.Name))
            {
                languages = languages.From(member.Value);
            }
            else if (member.NameEquals(routes.Name))
            {
                routes = routes.From(member.Value);
            }
            else if (member.NameEquals(schemaVersions.Name))
            {
                schemaVersions = schemaVersions.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ApiVersion
        {
            Languages = languages.SelectMany(value => value.GetStringRequired()),
            Routes = routes.SelectMany(value => ReadApiRoute(value, missingMemberBehavior)),
            SchemaVersions = schemaVersions.SelectMany(value => ReadSchema(value, missingMemberBehavior)) ??
                Array.Empty<Schema>()
        };
    }

    private static Route ReadApiRoute(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> path = new("path");
        RequiredMember<bool> lang = new("lang");
        OptionalMember<bool> auth = new("auth");
        RequiredMember<bool> active = new("active");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(path.Name))
            {
                path = path.From(member.Value);
            }
            else if (member.NameEquals(lang.Name))
            {
                lang = lang.From(member.Value);
            }
            else if (member.NameEquals(auth.Name))
            {
                auth = auth.From(member.Value);
            }
            else if (member.NameEquals(active.Name))
            {
                active = active.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Route
        {
            Path = path.GetValue(),
            Multilingual = lang.GetValue(),
            RequiresAuthorization = auth.GetValue(),
            Active = active.GetValue()
        };
    }

    private static Schema ReadSchema(JsonElement jsonElement, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> version = new("v");

        RequiredMember<string> description = new("desc");

        foreach (var member in jsonElement.EnumerateObject())
        {
            if (member.NameEquals(version.Name))
            {
                version = version.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Schema
        {
            Version = version.GetValue(),
            Description = description.GetValue()
        };
    }
}
