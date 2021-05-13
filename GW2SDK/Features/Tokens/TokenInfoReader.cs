using System;
using System.Text.Json;
using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    public sealed class TokenInfoReader : ITokenInfoReader,
        IJsonReader<ApiKeyInfo>,
        IJsonReader<SubtokenInfo>
    {
        public TokenInfo Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            switch (json.GetProperty("type").GetString())
            {
                case "APIKey":
                    return ((IJsonReader<ApiKeyInfo>)this).Read(json, missingMemberBehavior);
                case "Subtoken":
                    return ((IJsonReader<SubtokenInfo>)this).Read(json, missingMemberBehavior);
            }

            var name = new RequiredMember<string>("name");
            var id = new RequiredMember<string>("id");
            var permissions = new RequiredMember<Permission[]>("permissions");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException($"Unexpected discriminator '{member.Value.GetString()}'.");
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new TokenInfo
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Permissions = permissions.GetValue()
            };
        }

        ApiKeyInfo IJsonReader<ApiKeyInfo>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var id = new RequiredMember<string>("id");
            var permissions = new RequiredMember<Permission[]>("permissions");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("APIKey"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new ApiKeyInfo
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Permissions = permissions.GetValue()
            };
        }

        SubtokenInfo IJsonReader<SubtokenInfo>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var id = new RequiredMember<string>("id");
            var permissions = new RequiredMember<Permission[]>("permissions");
            var expiresAt = new RequiredMember<DateTimeOffset>("expires_at");
            var issuedAt = new RequiredMember<DateTimeOffset>("issued_at");
            var urls = new OptionalMember<Uri[]>("urls");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Subtoken"))
                    {
                        throw new InvalidOperationException($"Invalid type '{member.Value.GetString()}'.");
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new SubtokenInfo
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Permissions = permissions.GetValue(),
                ExpiresAt = expiresAt.GetValue(),
                IssuedAt = issuedAt.GetValue(),
                Urls = urls.Select(value => value.GetArray(
                    item => new Uri(item.GetStringRequired(), UriKind.Relative)))
            };
        }
    }
}
