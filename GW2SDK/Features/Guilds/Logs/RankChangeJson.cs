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
        RequiredMember id = new("id");
        RequiredMember time = new("time");
        RequiredMember user = new("user");
        OptionalMember changedBy = new("changed_by");
        RequiredMember oldRank = new("old_rank");
        RequiredMember newRank = new("new_rank");

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
                id = member;
            }
            else if (member.NameEquals(time.Name))
            {
                time = member;
            }
            else if (member.NameEquals(user.Name))
            {
                user = member;
            }
            else if (member.NameEquals(changedBy.Name))
            {
                changedBy = member;
            }
            else if (member.NameEquals(oldRank.Name))
            {
                oldRank = member;
            }
            else if (member.NameEquals(newRank.Name))
            {
                newRank = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RankChange
        {
            Id = id.Select(value => value.GetInt32()),
            Time = time.Select(value => value.GetDateTimeOffset()),
            User = user.Select(value => value.GetStringRequired()),
            ChangedBy = changedBy.Select(value => value.GetString()) ?? "",
            OldRank = oldRank.Select(value => value.GetStringRequired()),
            NewRank = newRank.Select(value => value.GetStringRequired())
        };
    }
}
