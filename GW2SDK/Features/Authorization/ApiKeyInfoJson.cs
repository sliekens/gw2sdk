using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Authorization;

internal static class ApiKeyInfoJson
{
    public static ApiKeyInfo GetApiKeyInfo(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember id = "id";
        RequiredMember permissions = "permissions";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("APIKey"))
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new ApiKeyInfo
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Permissions = permissions.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetEnum<Permission>())
            )
        };
    }
}
