using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Tokens;

[PublicAPI]
public static class TokenInfoJson
{
    public static TokenInfo GetTokenInfo(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "APIKey":
                return json.GetApiKeyInfo(missingMemberBehavior);
            case "Subtoken":
                return json.GetSubtokenInfo(missingMemberBehavior);
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
}
