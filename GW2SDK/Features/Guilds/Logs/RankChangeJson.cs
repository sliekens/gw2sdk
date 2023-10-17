using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

[PublicAPI]
public static class RankChangeJson
{
    public static RankChange GetRankChange(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<DateTimeOffset> time = new("time");
        RequiredMember<string> user = new("user");
        OptionalMember<string> changedBy = new("changed_by");
        RequiredMember<string> oldRank = new("old_rank");
        RequiredMember<string> newRank = new("new_rank");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("rank_change"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(time.Name))
            {
                time.Value = member.Value;
            }
            else if (member.NameEquals(user.Name))
            {
                user.Value = member.Value;
            }
            else if (member.NameEquals(changedBy.Name))
            {
                changedBy.Value = member.Value;
            }
            else if (member.NameEquals(oldRank.Name))
            {
                oldRank.Value = member.Value;
            }
            else if (member.NameEquals(newRank.Name))
            {
                newRank.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RankChange
        {
            Id = id.GetValue(),
            Time = time.GetValue(),
            User = user.GetValue(),
            ChangedBy = changedBy.GetValueOrEmpty(),
            OldRank = oldRank.GetValue(),
            NewRank = newRank.GetValue()
        };
    }
}
