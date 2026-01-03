using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Files;

internal static class AssetJson
{
    public static Asset GetAsset(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember icon = "icon";

        foreach (JsonProperty member in json.EnumerateObject())
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
            Id = id.Map(static (in value) => value.GetStringRequired()),
            IconUrl = new Uri(icon.Map(static (in value) => value.GetStringRequired()))
        };
    }
}
