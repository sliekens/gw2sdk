using System;
using System.Text.Json;
using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Accounts
{
    [PublicAPI]
    public sealed class AccountReader : IAccountReader
    {
        public Account Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var id = new RequiredMember<string>("id");
            var name = new RequiredMember<string>("name");
            var age = new RequiredMember<TimeSpan>("age");
            var lastModified = new RequiredMember<DateTimeOffset>("last_modified");
            var world = new RequiredMember<int>("world");
            var guilds = new RequiredMember<string[]>("guilds");
            var guildLeader = new OptionalMember<string[]>("guild_leader");
            var created = new RequiredMember<DateTimeOffset>("created");
            var access = new RequiredMember<ProductName[]>("access");
            var commander = new RequiredMember<bool>("commander");
            var fractalLevel = new NullableMember<int>("fractal_level");
            var dailyAp = new NullableMember<int>("daily_ap");
            var monthlyAp = new NullableMember<int>("monthly_ap");
            var wvwRank = new NullableMember<int>("wvw_rank");
            var buildStorageSlots = new NullableMember<int>("build_storage_slots");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(age.Name))
                {
                    age = age.From(member.Value);
                }
                else if (member.NameEquals(lastModified.Name))
                {
                    lastModified = lastModified.From(member.Value);
                }
                else if (member.NameEquals(world.Name))
                {
                    world = world.From(member.Value);
                }
                else if (member.NameEquals(guilds.Name))
                {
                    guilds = guilds.From(member.Value);
                }
                else if (member.NameEquals(guildLeader.Name))
                {
                    guildLeader = guildLeader.From(member.Value);
                }
                else if (member.NameEquals(created.Name))
                {
                    created = created.From(member.Value);
                }
                else if (member.NameEquals(access.Name))
                {
                    access = access.From(member.Value);
                }
                else if (member.NameEquals(commander.Name))
                {
                    commander = commander.From(member.Value);
                }
                else if (member.NameEquals(fractalLevel.Name))
                {
                    fractalLevel = fractalLevel.From(member.Value);
                }
                else if (member.NameEquals(dailyAp.Name))
                {
                    dailyAp = dailyAp.From(member.Value);
                }
                else if (member.NameEquals(monthlyAp.Name))
                {
                    monthlyAp = monthlyAp.From(member.Value);
                }
                else if (member.NameEquals(wvwRank.Name))
                {
                    wvwRank = wvwRank.From(member.Value);
                }
                else if (member.NameEquals(buildStorageSlots.Name))
                {
                    buildStorageSlots = buildStorageSlots.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Account
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Age = age.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
                LastModified = lastModified.GetValue(),
                World = world.GetValue(),
                Guilds = guilds.Select(value => value.GetArray(item => item.GetStringRequired())),
                GuildLeader = guildLeader.Select(value => value.GetArray(item => item.GetStringRequired())),
                Created = created.GetValue(),
                Access = access.GetValue(),
                Commander = commander.GetValue(),
                FractalLevel = fractalLevel.GetValue(),
                DailyAp = dailyAp.GetValue(),
                MonthlyAp = monthlyAp.GetValue(),
                WvwRank = wvwRank.GetValue(),
                BuildStorageSlots = buildStorageSlots.GetValue()
            };
        }
    }
}
