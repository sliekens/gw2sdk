using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Cats;

internal static class CatJson
{
    public static Cat GetCat(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember hint = "hint";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (hint.Match(member))
            {
                hint = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Cat
        {
            Id = id.Map(value => value.GetInt32()),
            Hint = hint.Map(value => value.GetStringRequired())
        };
    }
}
