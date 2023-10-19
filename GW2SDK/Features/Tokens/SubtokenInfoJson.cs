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
        RequiredMember name = new("name");
        RequiredMember id = new("id");
        RequiredMember permissions = new("permissions");
        RequiredMember expiresAt = new("expires_at");
        RequiredMember issuedAt = new("issued_at");
        OptionalMember urls = new("urls");
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
            Id = id.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired()),
            Permissions = permissions.SelectMany(value => value.GetEnum<Permission>(missingMemberBehavior)),
            ExpiresAt = expiresAt.Select(value => value.GetDateTimeOffset()),
            IssuedAt = issuedAt.Select(value => value.GetDateTimeOffset()),
            Urls = urls.SelectMany(item => new Uri(item.GetStringRequired(), UriKind.Relative))
        };
    }
}
