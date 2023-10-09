using System.Text.Json;
using GuildWars2;
using GuildWars2.Json;

namespace GW2SDK.Features.Accounts;

[PublicAPI]
public static class ProgressionJson
{
    public static Progression GetProgression(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<int> value = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(value.Name))
            {
                value.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Progression
        {
            Id = id.GetValue(),
            Value = value.GetValue()
        };
    }
}
