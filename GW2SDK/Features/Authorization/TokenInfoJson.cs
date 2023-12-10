using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Authorization;

internal static class TokenInfoJson
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
            if (member.Name == "type")
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == permissions.Name)
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
