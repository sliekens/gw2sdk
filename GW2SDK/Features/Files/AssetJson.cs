using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Files;

[PublicAPI]
public static class AssetJson
{
    public static Asset GetAsset(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember icon = "icon";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Asset
        {
            Id = id.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired())
        };
    }
}
