using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
public static class RankJson
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(finisherId.Name))
            {
                finisherId = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(minRank.Name))
            {
                minRank = member;
            }
            else if (member.NameEquals(maxRank.Name))
            {
                maxRank = member;
            }
            else if (member.NameEquals(levels.Name))
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
            Icon = icon.Map(value => value.GetStringRequired()),
            MinRank = minRank.Map(value => value.GetInt32()),
            MaxRank = maxRank.Map(value => value.GetInt32()),
            Levels = levels.Map(values => values.GetList(value => value.GetLevel(missingMemberBehavior)))
        };
    }
}
