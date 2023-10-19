using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Home.Cats;

[PublicAPI]
public static class CatJson
{
    public static Cat GetCat(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember hint = new("hint");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(hint.Name))
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
            Id = id.Select(value => value.GetInt32()),
            Hint = hint.Select(value => value.GetStringRequired())
        };
    }
}
