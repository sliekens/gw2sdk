using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Logs;

internal sealed class GuildLogEntryJsonConverter : JsonConverter<GuildLogEntry>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "guild_log_entry";

    public override GuildLogEntry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildLogEntry value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildLogEntry Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case GuildBankActivityJsonConverter.DiscriminatorValue:
                    return GuildBankActivityJsonConverter.Read(json);
                case GuildMissionJsonConverter.DiscriminatorValue:
                    return GuildMissionJsonConverter.Read(json);
                case GuildUpgradeActivityJsonConverter.DiscriminatorValue:
                    return GuildUpgradeActivityJsonConverter.Read(json);
                case InfluenceActivityJsonConverter.DiscriminatorValue:
                    return InfluenceActivityJsonConverter.Read(json);
                case InviteDeclinedJsonConverter.DiscriminatorValue:
                    return InviteDeclinedJsonConverter.Read(json);
                case MemberInvitedJsonConverter.DiscriminatorValue:
                    return MemberInvitedJsonConverter.Read(json);
                case MemberJoinedJsonConverter.DiscriminatorValue:
                    return MemberJoinedJsonConverter.Read(json);
                case MemberKickedJsonConverter.DiscriminatorValue:
                    return MemberKickedJsonConverter.Read(json);
                case NewMessageOfTheDayJsonConverter.DiscriminatorValue:
                    return NewMessageOfTheDayJsonConverter.Read(json);
                case RankChangeJsonConverter.DiscriminatorValue:
                    return RankChangeJsonConverter.Read(json);
                case TreasuryDepositJsonConverter.DiscriminatorValue:
                    return TreasuryDepositJsonConverter.Read(json);
                case DiscriminatorValue:
                    break;
                default:
                    ThrowHelper.ThrowInvalidDiscriminator(discriminator.GetString());
                    break;
            }
        }
        JsonElement idJson = json.GetProperty("id");
        JsonElement timeJson = json.GetProperty("time");
        return new GuildLogEntry
        {
            Id = idJson.GetInt32(),
            Time = timeJson.GetDateTimeOffset()
        };
    }

    public static void Write(Utf8JsonWriter writer, GuildLogEntry value)
    {
        if (value is GuildBankActivity guildBankActivity)
        {
            GuildBankActivityJsonConverter.Write(writer, guildBankActivity);
        }
        else if (value is GuildMission guildMission)
        {
            GuildMissionJsonConverter.Write(writer, guildMission);
        }
        else if (value is GuildUpgradeActivity guildUpgradeActivity)
        {
            GuildUpgradeActivityJsonConverter.Write(writer, guildUpgradeActivity);
        }
        else if (value is InfluenceActivity influenceActivity)
        {
            InfluenceActivityJsonConverter.Write(writer, influenceActivity);
        }
        else if (value is InviteDeclined inviteDeclined)
        {
            InviteDeclinedJsonConverter.Write(writer, inviteDeclined);
        }
        else if (value is MemberInvited memberInvited)
        {
            MemberInvitedJsonConverter.Write(writer, memberInvited);
        }
        else if (value is MemberJoined memberJoined)
        {
            MemberJoinedJsonConverter.Write(writer, memberJoined);
        }
        else if (value is MemberKicked memberKicked)
        {
            MemberKickedJsonConverter.Write(writer, memberKicked);
        }
        else if (value is NewMessageOfTheDay newMessageOfTheDay)
        {
            NewMessageOfTheDayJsonConverter.Write(writer, newMessageOfTheDay);
        }
        else if (value is RankChange rankChange)
        {
            RankChangeJsonConverter.Write(writer, rankChange);
        }
        else if (value is TreasuryDeposit treasuryDeposit)
        {
            TreasuryDepositJsonConverter.Write(writer, treasuryDeposit);
        }
        else if (value.GetType() == typeof(GuildLogEntry))
        {
            writer.WriteStartObject();
            writer.WriteString(DiscriminatorName, DiscriminatorValue);
            writer.WriteNumber("id", value.Id);
            writer.WriteString("time", value.Time);
            writer.WriteEndObject();
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("No JSON converter is registered for " + value.GetType() + ".");
        }
    }
}
