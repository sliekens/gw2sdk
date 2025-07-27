using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Metadata;

internal static class RouteJson
{
    public static Route GetRoute(this in JsonElement json)
    {
        RequiredMember path = "path";
        RequiredMember lang = "lang";
        OptionalMember auth = "auth";
        RequiredMember active = "active";

        foreach (var member in json.EnumerateObject())
        {
            if (path.Match(member))
            {
                path = member;
            }
            else if (lang.Match(member))
            {
                lang = member;
            }
            else if (auth.Match(member))
            {
                auth = member;
            }
            else if (active.Match(member))
            {
                active = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Route
        {
            Path = path.Map(static (in JsonElement value) => value.GetStringRequired()),
            Multilingual = lang.Map(static (in JsonElement value) => value.GetBoolean()),
            RequiresAuthorization = auth.Map(static (in JsonElement value) => value.GetBoolean()),
            Active = active.Map(static (in JsonElement value) => value.GetBoolean())
        };
    }
}
