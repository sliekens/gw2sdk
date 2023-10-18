using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

[PublicAPI]
public enum InfluenceActivityKind
{
    Gifted = 1
}

[PublicAPI]
public static class InfluenceActivityJson
{
    public static InfluenceActivity GetInfluenceActivity(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<DateTimeOffset> time = new("time");
        RequiredMember<InfluenceActivityKind> activity = new("activity");
        RequiredMember<int> totalParticipants = new("total_participants");
        RequiredMember<string> participants = new("participants");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("influence"))
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
            else if (member.NameEquals(activity.Name))
            {
                activity.Value = member.Value;
            }
            else if (member.NameEquals(totalParticipants.Name))
            {
                totalParticipants.Value = member.Value;
            }
            else if (member.NameEquals(participants.Name))
            {
                participants.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InfluenceActivity
        {
            Id = id.GetValue(),
            Time = time.GetValue(),
            Activity = activity.GetValue(missingMemberBehavior),
            TotalParticipants = totalParticipants.GetValue(),
            Participants = participants.SelectMany(value => value.GetStringRequired())
        };
    }
}
