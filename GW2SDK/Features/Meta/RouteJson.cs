using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Meta;

[PublicAPI]
public static class RouteJson
{
    public static Route GetRoute(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember path = new("path");
        RequiredMember lang = new("lang");
        OptionalMember auth = new("auth");
        RequiredMember active = new("active");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(path.Name))
            {
                path.Value = member.Value;
            }
            else if (member.NameEquals(lang.Name))
            {
                lang.Value = member.Value;
            }
            else if (member.NameEquals(auth.Name))
            {
                auth.Value = member.Value;
            }
            else if (member.NameEquals(active.Name))
            {
                active.Value = member.Value;
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
