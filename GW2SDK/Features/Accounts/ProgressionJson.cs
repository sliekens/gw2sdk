using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Accounts;

[PublicAPI]
public static class ProgressionJson
{
    public static Progression GetProgression(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember progressId = new("id");
        RequiredMember progress = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(progressId.Name))
            {
                progressId.Value = member.Value;
            }
            else if (member.NameEquals(progress.Name))
            {
                progress.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Progression
        {
            Id = progressId.Select(value => value.GetStringRequired()),
            Value = progress.Select(value => value.GetInt32())
        };
    }
}
