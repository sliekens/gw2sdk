using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Meta;

[PublicAPI]
public static class RouteJson
{
    public static Route GetRoute(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> path = new("path");
        RequiredMember<bool> lang = new("lang");
        OptionalMember<bool> auth = new("auth");
        RequiredMember<bool> active = new("active");

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
            Path = path.GetValue(),
            Multilingual = lang.GetValue(),
            RequiresAuthorization = auth.GetValue(),
            Active = active.GetValue()
        };
    }
}
