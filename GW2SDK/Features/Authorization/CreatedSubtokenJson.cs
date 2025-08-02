using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Authorization;

internal static class CreatedSubtokenJson
{
    public static CreatedSubtoken GetCreatedSubtoken(this in JsonElement json)
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new CreatedSubtoken
        {
            Subtoken = subtoken.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}
