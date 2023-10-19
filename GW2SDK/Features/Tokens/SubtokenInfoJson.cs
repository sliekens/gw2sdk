using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Tokens;

[PublicAPI]
public static class SubtokenInfoJson
{
    public static SubtokenInfo GetSubtokenInfo(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember id = "id";
        RequiredMember permissions = "permissions";
        RequiredMember expiresAt = "expires_at";
        RequiredMember issuedAt = "issued_at";
        OptionalMember urls = "urls";
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
            else if (member.NameEquals(expiresAt.Name))
            {
                expiresAt = member;
            }
            else if (member.NameEquals(issuedAt.Name))
            {
                issuedAt = member;
            }
            else if (member.NameEquals(urls.Name))
            {
                urls = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SubtokenInfo
        {
            Id = id.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Permissions = permissions.Map(values => values.GetList(value => value.GetEnum<Permission>(missingMemberBehavior))),
            ExpiresAt = expiresAt.Map(value => value.GetDateTimeOffset()),
            IssuedAt = issuedAt.Map(value => value.GetDateTimeOffset()),
            Urls = urls.Map(values => values.GetList(item => new Uri(item.GetStringRequired(), UriKind.Relative)))
        };
    }
}
