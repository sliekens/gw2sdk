using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Files;

internal static class AssetJson
{
    public static Asset GetAsset(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember icon = "icon";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Asset
        {
            Id = id.Map(static value => value.GetStringRequired()),
            IconHref = icon.Map(static value => value.GetStringRequired())
        };
    }
}
