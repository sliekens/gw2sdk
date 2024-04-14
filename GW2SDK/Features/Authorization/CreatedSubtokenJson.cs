using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Authorization;

internal static class CreatedSubtokenJson
{
    public static CreatedSubtoken GetCreatedSubtoken(
        this JsonElement json
    )
    {
        RequiredMember subtoken = "subtoken";

        foreach (var member in json.EnumerateObject())
        {
            if (subtoken.Match(member))
            {
                subtoken = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CreatedSubtoken { Subtoken = subtoken.Map(static value => value.GetStringRequired()) };
    }
}
