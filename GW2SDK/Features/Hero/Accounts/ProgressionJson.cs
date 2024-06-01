using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class ProgressionJson
{
    public static Progression GetProgression(this JsonElement json)
    {
        RequiredMember progressId = "id";
        RequiredMember progress = "value";

        foreach (var member in json.EnumerateObject())
        {
            if (progressId.Match(member))
            {
                progressId = member;
            }
            else if (progress.Match(member))
            {
                progress = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Progression
        {
            Id = progressId.Map(static value => value.GetStringRequired()),
            Value = progress.Map(static value => value.GetInt32())
        };
    }
}
