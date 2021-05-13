using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.V2
{
    [PublicAPI]
    public sealed class ApiInfoReader : IApiInfoReader
    {
        public ApiInfo Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior)
        {
            var languages = new RequiredMember<string[]>("langs");
            var routes = new RequiredMember<ApiRoute[]>("routes");
            var schemaVersions = new RequiredMember<ApiVersion[]>("schema_versions");

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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ApiInfo
            {
                Languages = languages.Select(value => ReadLanguages(value)),
                Routes = routes.Select(value => ReadRoutes(value, missingMemberBehavior)), 
                SchemaVersions = schemaVersions.Select(value => ReadApiVersions(value, missingMemberBehavior))
            };
        }

        private string[] ReadLanguages(JsonElement value)
        {
            return value.GetArray(item => item.GetStringRequired());
        }

        private ApiRoute[] ReadRoutes(JsonElement value, MissingMemberBehavior missingMemberBehavior)
        {
            return value.GetArray(item => ReadApiRoute(item, missingMemberBehavior));
        }

        private ApiRoute ReadApiRoute(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var path = new RequiredMember<string>("path");
            var lang = new RequiredMember<bool>("lang");
            var auth = new RequiredMember<bool>("auth");
            var active = new RequiredMember<bool>("active");

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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ApiRoute { Path = path.GetValue(), Multilingual = lang.GetValue(), RequiresAuthorization = auth.GetValue(), Active = active.GetValue() };
        }

        private ApiVersion[] ReadApiVersions(JsonElement value, MissingMemberBehavior missingMemberBehavior)
        {
            return value.GetArray(item => ReadApiVersion(item, missingMemberBehavior));
        }

        private ApiVersion ReadApiVersion(JsonElement jsonElement, MissingMemberBehavior missingMemberBehavior)
        {
            var version = new RequiredMember<string>("v");

            var description = new RequiredMember<string>("desc");

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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ApiVersion { Version = version.GetValue(), Description = description.GetValue() };
        }
    }
}
