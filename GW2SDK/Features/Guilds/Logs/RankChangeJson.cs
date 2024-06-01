using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class RankChangeJson
{
    public static RankChange GetRankChange(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember user = "user";
        OptionalMember changedBy = "changed_by";
        RequiredMember oldRank = "old_rank";
        RequiredMember newRank = "new_rank";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("rank_change"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (time.Match(member))
            {
                time = member;
            }
            else if (user.Match(member))
            {
                user = member;
            }
            else if (changedBy.Match(member))
            {
                changedBy = member;
            }
            else if (oldRank.Match(member))
            {
                oldRank = member;
            }
            else if (newRank.Match(member))
            {
                newRank = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new RankChange
        {
            Id = id.Map(static value => value.GetInt32()),
            Time = time.Map(static value => value.GetDateTimeOffset()),
            User = user.Map(static value => value.GetStringRequired()),
            ChangedBy = changedBy.Map(static value => value.GetString()) ?? "",
            OldRank = oldRank.Map(static value => value.GetStringRequired()),
            NewRank = newRank.Map(static value => value.GetStringRequired())
        };
    }
}
