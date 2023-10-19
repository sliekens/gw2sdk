using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Meta;

[PublicAPI]
public static class RouteJson
{
    public static Route GetRoute(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember path = "path";
        RequiredMember lang = "lang";
        OptionalMember auth = "auth";
        RequiredMember active = "active";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(path.Name))
            {
                path = member;
            }
            else if (member.NameEquals(lang.Name))
            {
                lang = member;
            }
            else if (member.NameEquals(auth.Name))
            {
                auth = member;
            }
            else if (member.NameEquals(active.Name))
            {
                active = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Route
        {
            Path = path.Select(value => value.GetStringRequired()),
            Multilingual = lang.Select(value => value.GetBoolean()),
            RequiresAuthorization = auth.Select(value => value.GetBoolean()),
            Active = active.Select(value => value.GetBoolean())
        };
    }
}
