using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Tokens;

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

        RequiredMember name = "name";
        RequiredMember id = "id";
        RequiredMember permissions = "permissions";
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
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(permissions.Name))
            {
                permissions = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TokenInfo
        {
            Id = id.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Permissions = permissions.Map(
                values => values.GetList(value => value.GetEnum<Permission>(missingMemberBehavior))
            )
        };
    }
}
