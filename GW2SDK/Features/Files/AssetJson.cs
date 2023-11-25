using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Files;

internal static class AssetJson
{
    public static Asset GetAsset(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember icon = "icon";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == icon.Name)
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
            Id = id.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetStringRequired())
        };
    }
}
