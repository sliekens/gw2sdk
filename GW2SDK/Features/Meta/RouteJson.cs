using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Meta;

internal static class RouteJson
{
    public static Route GetRoute(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember path = "path";
        RequiredMember lang = "lang";
        OptionalMember auth = "auth";
        RequiredMember active = "active";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == path.Name)
            {
                path = member;
            }
            else if (member.Name == lang.Name)
            {
                lang = member;
            }
            else if (member.Name == auth.Name)
            {
                auth = member;
            }
            else if (member.Name == active.Name)
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
            Path = path.Map(value => value.GetStringRequired()),
            Multilingual = lang.Map(value => value.GetBoolean()),
            RequiresAuthorization = auth.Map(value => value.GetBoolean()),
            Active = active.Map(value => value.GetBoolean())
        };
    }
}
