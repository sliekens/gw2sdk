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

        var iconString = icon.Map(static value => value.GetStringRequired());
        return new Asset
        {
            Id = id.Map(static value => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute)
        };
    }
}
