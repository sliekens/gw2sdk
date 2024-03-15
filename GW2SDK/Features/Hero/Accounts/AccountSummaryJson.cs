using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class AccountSummaryJson
{
    public static AccountSummary GetAccountSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
        NullableMember wvwRank = "wvw_rank";
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
            else if (wvwRank.Match(member))
            {
                wvwRank = member;
            }
            else if (buildStorageSlots.Match(member))
            {
                buildStorageSlots = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AccountSummary
        {
            Id = id.Map(value => value.GetStringRequired()),
            DisplayName = name.Map(value => value.GetStringRequired()),
            Age = age.Map(value => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.Map(value => value.GetDateTimeOffset()),
            WorldId = world.Map(value => value.GetInt32()),
            GuildIds = guilds.Map(values => values.GetList(value => value.GetStringRequired())),
            LeaderOfGuildIds =
                guildLeader.Map(values => values.GetList(value => value.GetStringRequired())),
            Created = created.Map(value => value.GetDateTimeOffset()),
            Access =
                access.Map(
                    values => values.GetList(
                        value => value.GetEnum<ProductName>(missingMemberBehavior)
                    )
                ),
            Commander = commander.Map(value => value.GetBoolean()),
            FractalLevel = fractalLevel.Map(value => value.GetInt32()),
            DailyAchievementPoints = dailyAp.Map(value => value.GetInt32()),
            MonthlyAchievementPoints = monthlyAp.Map(value => value.GetInt32()),
            WvwRank = wvwRank.Map(value => value.GetInt32()),
            BuildStorageSlots = buildStorageSlots.Map(value => value.GetInt32())
        };
    }
}
