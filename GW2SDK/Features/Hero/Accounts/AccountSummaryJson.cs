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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == age.Name)
            {
                age = member;
            }
            else if (member.Name == lastModified.Name)
            {
                lastModified = member;
            }
            else if (member.Name == world.Name)
            {
                world = member;
            }
            else if (member.Name == guilds.Name)
            {
                guilds = member;
            }
            else if (member.Name == guildLeader.Name)
            {
                guildLeader = member;
            }
            else if (member.Name == created.Name)
            {
                created = member;
            }
            else if (member.Name == access.Name)
            {
                access = member;
            }
            else if (member.Name == commander.Name)
            {
                commander = member;
            }
            else if (member.Name == fractalLevel.Name)
            {
                fractalLevel = member;
            }
            else if (member.Name == dailyAp.Name)
            {
                dailyAp = member;
            }
            else if (member.Name == monthlyAp.Name)
            {
                monthlyAp = member;
            }
            else if (member.Name == wvwRank.Name)
            {
                wvwRank = member;
            }
            else if (member.Name == buildStorageSlots.Name)
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
