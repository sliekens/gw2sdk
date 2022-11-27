using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts;

[PublicAPI]
public static class AccountSummaryJson
{
    public static AccountSummary GetAccountSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<TimeSpan> age = new("age");
        RequiredMember<DateTimeOffset> lastModified = new("last_modified");
        RequiredMember<int> world = new("world");
        RequiredMember<string> guilds = new("guilds");
        OptionalMember<string> guildLeader = new("guild_leader");
        RequiredMember<DateTimeOffset> created = new("created");
        RequiredMember<ProductName> access = new("access");
        RequiredMember<bool> commander = new("commander");
        NullableMember<int> fractalLevel = new("fractal_level");
        NullableMember<int> dailyAp = new("daily_ap");
        NullableMember<int> monthlyAp = new("monthly_ap");
        NullableMember<int> wvwRank = new("wvw_rank");
        NullableMember<int> buildStorageSlots = new("build_storage_slots");
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
            Id = id.GetValue(),
            DisplayName = name.GetValue(),
            Age = age.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.GetValue(),
            WorldId = world.GetValue(),
            GuildIds = guilds.SelectMany(value => value.GetStringRequired()),
            LeaderOfGuildIds = guildLeader.SelectMany(value => value.GetStringRequired()),
            Created = created.GetValue(),
            Access = access.GetValues(missingMemberBehavior),
            Commander = commander.GetValue(),
            FractalLevel = fractalLevel.GetValue(),
            DailyAchievementPoints = dailyAp.GetValue(),
            MonthlyAchievementPoints = monthlyAp.GetValue(),
            WvwRank = wvwRank.GetValue(),
            BuildStorageSlots = buildStorageSlots.GetValue()
        };
    }
}
