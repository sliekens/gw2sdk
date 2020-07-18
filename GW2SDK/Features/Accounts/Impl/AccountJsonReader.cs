using GW2SDK.Enums;
using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Accounts.Impl
{
    internal sealed class AccountJsonReader : JsonObjectReader<Account>
    {
        private AccountJsonReader()
        {
            Map("id",                  to => to.Id);
            Map("name",                to => to.Name);
            Map("age",                 to => to.Age, SecondsJsonReader.Instance);
            Map("last_modified",       to => to.LastModified);
            Map("world",               to => to.World);
            Map("guilds",              to => to.Guilds);
            Map("guild_leader",        to => to.GuildLeader, PropertySignificance.Optional);
            Map("created",             to => to.Created);
            Map("access",              to => to.Access, new JsonStringEnumReader<ProductName>());
            Map("commander",           to => to.Commander);
            Map("fractal_level",       to => to.FractalLevel);
            Map("daily_ap",            to => to.DailyAp);
            Map("monthly_ap",          to => to.MonthlyAp);
            Map("wvw_rank",            to => to.WvwRank);
            Map("build_storage_slots", to => to.BuildStorageSlots);
            Compile();
        }

        internal static AccountJsonReader Instance { get; } = new AccountJsonReader();
    }
}
