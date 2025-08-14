using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Cats;

internal static class CatJson
{
    public static Cat GetCat(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember hint = "hint";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (hint.Match(member))
            {
                hint = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Cat
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Hint = hint.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}
