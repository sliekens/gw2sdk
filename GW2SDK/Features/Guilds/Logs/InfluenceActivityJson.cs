using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class InfluenceActivityJson
{
    public static InfluenceActivity GetInfluenceActivity(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember activity = "activity";
        RequiredMember totalParticipants = "total_participants";
        RequiredMember participants = "participants";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("influence"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == time.Name)
            {
                time = member;
            }
            else if (member.Name == activity.Name)
            {
                activity = member;
            }
            else if (member.Name == totalParticipants.Name)
            {
                totalParticipants = member;
            }
            else if (member.Name == participants.Name)
            {
                participants = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InfluenceActivity
        {
            Id = id.Map(value => value.GetInt32()),
            Time = time.Map(value => value.GetDateTimeOffset()),
            Activity =
                activity.Map(value => value.GetEnum<InfluenceActivityKind>(missingMemberBehavior)),
            TotalParticipants = totalParticipants.Map(value => value.GetInt32()),
            Participants =
                participants.Map(values => values.GetList(value => value.GetStringRequired()))
        };
    }
}
