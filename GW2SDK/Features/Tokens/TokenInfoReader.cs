using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Tokens;

[PublicAPI]
public static class TokenInfoReader
{
    public static TokenInfo GetTokenInfo(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "APIKey":
                return ReadApiKeyInfo(json, missingMemberBehavior);
            case "Subtoken":
                return ReadSubtokenInfo(json, missingMemberBehavior);
        }

        RequiredMember<string> name = new("name");
        RequiredMember<string> id = new("id");
        RequiredMember<Permission> permissions = new("permissions");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(permissions.Name))
            {
                permissions.Value = member.Value;
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

    private static ApiKeyInfo ReadApiKeyInfo(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        RequiredMember<string> id = new("id");
        RequiredMember<Permission> permissions = new("permissions");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("APIKey"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(permissions.Name))
            {
                permissions.Value = member.Value;
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

    private static SubtokenInfo ReadSubtokenInfo(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        RequiredMember<string> id = new("id");
        RequiredMember<Permission> permissions = new("permissions");
        RequiredMember<DateTimeOffset> expiresAt = new("expires_at");
        RequiredMember<DateTimeOffset> issuedAt = new("issued_at");
        OptionalMember<Uri> urls = new("urls");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Subtoken"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(permissions.Name))
            {
                permissions.Value = member.Value;
            }
            else if (member.NameEquals(expiresAt.Name))
            {
                expiresAt.Value = member.Value;
            }
            else if (member.NameEquals(issuedAt.Name))
            {
                issuedAt.Value = member.Value;
            }
            else if (member.NameEquals(urls.Name))
            {
                urls.Value = member.Value;
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
