using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Logos;

internal static class LogoJson
{
    public static Logo GetLogo(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember url = "url";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (url.Match(member))
            {
                url = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Logo
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Url = new Uri(url.Map(static (in JsonElement value) => value.GetStringRequired()))
        };
    }
}
