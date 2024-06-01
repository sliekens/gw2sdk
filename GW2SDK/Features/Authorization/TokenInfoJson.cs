using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Authorization;

internal static class TokenInfoJson
{
    public static TokenInfo GetTokenInfo(this JsonElement json)
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "APIKey":
                    return json.GetApiKeyInfo();
                case "Subtoken":
                    return json.GetSubtokenInfo();
            }
        }

        RequiredMember name = "name";
        RequiredMember id = "id";
        RequiredMember permissions = "permissions";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (permissions.Match(member))
            {
                permissions = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new TokenInfo
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Name = name.Map(static value => value.GetStringRequired()),
            Permissions = permissions.Map(
                static values => values.GetList(static value => value.GetEnum<Permission>())
            )
        };
    }
}
