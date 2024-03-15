using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Ranks;

internal static class RankJson
{
    public static Rank GetRank(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember finisherId = "finisher_id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember minRank = "min_rank";
        RequiredMember maxRank = "max_rank";
        RequiredMember levels = "levels";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (finisherId.Match(member))
            {
                finisherId = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (minRank.Match(member))
            {
                minRank = member;
            }
            else if (maxRank.Match(member))
            {
                maxRank = member;
            }
            else if (levels.Match(member))
            {
                levels = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Rank
        {
            Id = id.Map(value => value.GetInt32()),
            FinisherId = finisherId.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            MinRank = minRank.Map(value => value.GetInt32()),
            MaxRank = maxRank.Map(value => value.GetInt32()),
            Levels = levels.Map(
                values => values.GetList(value => value.GetLevel(missingMemberBehavior))
            )
        };
    }
}
