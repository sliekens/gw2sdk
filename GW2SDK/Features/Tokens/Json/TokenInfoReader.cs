using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Tokens.Json
{
    [PublicAPI]
    public static class TokenInfoReader
    {
        public static TokenInfo Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("type").GetString())
            {
                case "APIKey":
                    return ReadApiKeyInfo(json, missingMemberBehavior);
                case "Subtoken":
                    return ReadSubtokenInfo(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var id = new RequiredMember<string>("id");
            var permissions = new RequiredMember<Permission>("permissions");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(permissions.Name))
                {
                    permissions = permissions.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new TokenInfo
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Permissions = permissions.GetValues(missingMemberBehavior)
            };
        }

        private static ApiKeyInfo ReadApiKeyInfo(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var id = new RequiredMember<string>("id");
            var permissions = new RequiredMember<Permission>("permissions");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("APIKey"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(permissions.Name))
                {
                    permissions = permissions.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new ApiKeyInfo
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Permissions = permissions.GetValues(missingMemberBehavior)
            };
        }

        private static SubtokenInfo ReadSubtokenInfo(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var id = new RequiredMember<string>("id");
            var permissions = new RequiredMember<Permission>("permissions");
            var expiresAt = new RequiredMember<DateTimeOffset>("expires_at");
            var issuedAt = new RequiredMember<DateTimeOffset>("issued_at");
            var urls = new OptionalMember<Uri>("urls");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Subtoken"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(permissions.Name))
                {
                    permissions = permissions.From(member.Value);
                }
                else if (member.NameEquals(expiresAt.Name))
                {
                    expiresAt = expiresAt.From(member.Value);
                }
                else if (member.NameEquals(issuedAt.Name))
                {
                    issuedAt = issuedAt.From(member.Value);
                }
                else if (member.NameEquals(urls.Name))
                {
                    urls = urls.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new SubtokenInfo
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Permissions = permissions.GetValues(missingMemberBehavior),
                ExpiresAt = expiresAt.GetValue(),
                IssuedAt = issuedAt.GetValue(),
                Urls = urls.SelectMany(item => new Uri(item.GetStringRequired(), UriKind.Relative))
            };
        }
    }
}
