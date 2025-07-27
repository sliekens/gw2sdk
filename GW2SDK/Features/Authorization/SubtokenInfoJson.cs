using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Authorization;

internal static class SubtokenInfoJson
{
    public static SubtokenInfo GetSubtokenInfo(this in JsonElement json)
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
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
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
            else if (expiresAt.Match(member))
            {
                expiresAt = member;
            }
            else if (issuedAt.Match(member))
            {
                issuedAt = member;
            }
            else if (urls.Match(member))
            {
                urls = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new SubtokenInfo
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Permissions =
                permissions.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetEnum<Permission>())
                ),
            ExpiresAt = expiresAt.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            IssuedAt = issuedAt.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            Urls = urls.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) =>
                    new Uri(value.GetStringRequired(), UriKind.Relative)
                )
            )
        };
    }
}
