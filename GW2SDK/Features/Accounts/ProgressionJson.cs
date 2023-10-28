using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Accounts;

internal static class ProgressionJson
{
    public static Progression GetProgression(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember progressId = "id";
        RequiredMember progress = "value";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == progressId.Name)
            {
                progressId = member;
            }
            else if (member.Name == progress.Name)
            {
                progress = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Progression
        {
            Id = progressId.Map(value => value.GetStringRequired()),
            Value = progress.Map(value => value.GetInt32())
        };
    }
}
