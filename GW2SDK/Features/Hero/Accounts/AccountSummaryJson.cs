using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class AccountSummaryJson
{
    public static AccountSummary GetAccountSummary(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember age = "age";
        RequiredMember lastModified = "last_modified";
        RequiredMember world = "world";
        RequiredMember guilds = "guilds";
        OptionalMember guildLeader = "guild_leader";
        RequiredMember created = "created";
        RequiredMember access = "access";
        RequiredMember commander = "commander";
        NullableMember fractalLevel = "fractal_level";
        NullableMember dailyAp = "daily_ap";
        NullableMember monthlyAp = "monthly_ap";
        RequiredMember wvw = "wvw";
        NullableMember buildStorageSlots = "build_storage_slots";
        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (age.Match(member))
            {
                age = member;
            }
            else if (lastModified.Match(member))
            {
                lastModified = member;
            }
            else if (world.Match(member))
            {
                world = member;
            }
            else if (guilds.Match(member))
            {
                guilds = member;
            }
            else if (guildLeader.Match(member))
            {
                guildLeader = member;
            }
            else if (created.Match(member))
            {
                created = member;
            }
            else if (access.Match(member))
            {
                access = member;
            }
            else if (commander.Match(member))
            {
                commander = member;
            }
            else if (fractalLevel.Match(member))
            {
                fractalLevel = member;
            }
            else if (dailyAp.Match(member))
            {
                dailyAp = member;
            }
            else if (monthlyAp.Match(member))
            {
                monthlyAp = member;
            }
            else if (wvw.Match(member))
            {
                wvw = member;
            }
            else if (buildStorageSlots.Match(member))
            {
                buildStorageSlots = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AccountSummary
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            DisplayName = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Age = age.Map(static (in JsonElement value) => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            WorldId = world.Map(static (in JsonElement value) => value.GetInt32()),
            GuildIds =
                guilds.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetStringRequired())
                ),
            LeaderOfGuildIds =
                guildLeader.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetStringRequired())
                ),
            Created = created.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            Access =
                access.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetEnum<ProductName>())
                ),
            Commander = commander.Map(static (in JsonElement value) => value.GetBoolean()),
            FractalLevel = fractalLevel.Map(static (in JsonElement value) => value.GetInt32()),
            DailyAchievementPoints = dailyAp.Map(static (in JsonElement value) => value.GetInt32()),
            MonthlyAchievementPoints = monthlyAp.Map(static (in JsonElement value) => value.GetInt32()),
            Wvw = wvw.Map(static (in JsonElement value) => value.GetAccountWvwSummary()),
            BuildStorageSlots = buildStorageSlots.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
