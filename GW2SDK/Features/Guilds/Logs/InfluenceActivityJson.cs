using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class InfluenceActivityJson
{
    public static InfluenceActivity GetInfluenceActivity(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember activity = "activity";
        RequiredMember totalParticipants = "total_participants";
        RequiredMember participants = "participants";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("influence"))
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
            else if (activity.Match(member))
            {
                activity = member;
            }
            else if (totalParticipants.Match(member))
            {
                totalParticipants = member;
            }
            else if (participants.Match(member))
            {
                participants = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new InfluenceActivity
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Time = time.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            Activity =
                activity.Map(static (in JsonElement value) => value.ValueEquals("daily_login")
                    ? InfluenceActivityKind.DailyLogin
                    : value.GetEnum<InfluenceActivityKind>()
                ),
            TotalParticipants = totalParticipants.Map(static (in JsonElement value) => value.GetInt32()),
            Participants = participants.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetStringRequired())
            )
        };
    }
}
