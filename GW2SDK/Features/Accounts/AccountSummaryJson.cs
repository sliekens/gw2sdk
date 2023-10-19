using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Accounts;

[PublicAPI]
public static class AccountSummaryJson
{
    public static AccountSummary GetAccountSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember age = new("age");
        RequiredMember lastModified = new("last_modified");
        RequiredMember world = new("world");
        RequiredMember guilds = new("guilds");
        OptionalMember guildLeader = new("guild_leader");
        RequiredMember created = new("created");
        RequiredMember access = new("access");
        RequiredMember commander = new("commander");
        NullableMember fractalLevel = new("fractal_level");
        NullableMember dailyAp = new("daily_ap");
        NullableMember monthlyAp = new("monthly_ap");
        NullableMember wvwRank = new("wvw_rank");
        NullableMember buildStorageSlots = new("build_storage_slots");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(age.Name))
            {
                age.Value = member.Value;
            }
            else if (member.NameEquals(lastModified.Name))
            {
                lastModified.Value = member.Value;
            }
            else if (member.NameEquals(world.Name))
            {
                world.Value = member.Value;
            }
            else if (member.NameEquals(guilds.Name))
            {
                guilds.Value = member.Value;
            }
            else if (member.NameEquals(guildLeader.Name))
            {
                guildLeader.Value = member.Value;
            }
            else if (member.NameEquals(created.Name))
            {
                created.Value = member.Value;
            }
            else if (member.NameEquals(access.Name))
            {
                access.Value = member.Value;
            }
            else if (member.NameEquals(commander.Name))
            {
                commander.Value = member.Value;
            }
            else if (member.NameEquals(fractalLevel.Name))
            {
                fractalLevel.Value = member.Value;
            }
            else if (member.NameEquals(dailyAp.Name))
            {
                dailyAp.Value = member.Value;
            }
            else if (member.NameEquals(monthlyAp.Name))
            {
                monthlyAp.Value = member.Value;
            }
            else if (member.NameEquals(wvwRank.Name))
            {
                wvwRank.Value = member.Value;
            }
            else if (member.NameEquals(buildStorageSlots.Name))
            {
                buildStorageSlots.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AccountSummary
        {
            Id = id.Select(value => value.GetStringRequired()),
            DisplayName = name.Select(value => value.GetStringRequired()),
            Age = age.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.Select(value => value.GetDateTimeOffset()),
            WorldId = world.Select(value => value.GetInt32()),
            GuildIds = guilds.SelectMany(value => value.GetStringRequired()),
            LeaderOfGuildIds = guildLeader.SelectMany(value => value.GetStringRequired()),
            Created = created.Select(value => value.GetDateTimeOffset()),
            Access = access.SelectMany(value => value.GetEnum<ProductName>(missingMemberBehavior)),
            Commander = commander.Select(value => value.GetBoolean()),
            FractalLevel = fractalLevel.Select(value => value.GetInt32()),
            DailyAchievementPoints = dailyAp.Select(value => value.GetInt32()),
            MonthlyAchievementPoints = monthlyAp.Select(value => value.GetInt32()),
            WvwRank = wvwRank.Select(value => value.GetInt32()),
            BuildStorageSlots = buildStorageSlots.Select(value => value.GetInt32())
        };
    }
}
