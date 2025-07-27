using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class GuildMissionJson
{
    public static GuildMission GetGuildMission(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember state = "state";
        RequiredMember influence = "influence";
        OptionalMember user = "user";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("mission"))
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
            else if (state.Match(member))
            {
                state = member;
            }
            else if (influence.Match(member))
            {
                influence = member;
            }
            else if (user.Match(member))
            {
                user = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildMission
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Time = time.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            User = user.Map(static (in JsonElement value) => value.GetString()) ?? "",
            State = state.Map(static (in JsonElement value) => value.GetEnum<GuildMissionState>()),
            Influence = influence.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
