using System;
using System.Text.Json;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Accounts.Impl
{
    internal sealed class AccountJsonReader : JsonObjectReader<Account>
    {
        private AccountJsonReader()
        {
            Configure(MapAccount);
        }

        internal static IJsonReader<Account> Instance { get; } = new AccountJsonReader();

        private static void MapAccount(JsonObjectMapping<Account> account)
        {
            account.Map("id", to => to.Id);
            account.Map("name", to => to.Name);
            account.Map("age", to => to.Age, (in JsonElement element, in JsonPath path) => TimeSpan.FromSeconds(element.GetDouble()));
            account.Map("last_modified", to => to.LastModified);
            account.Map("world", to => to.World);
            account.Map("guilds", to => to.Guilds);
            account.Map("guild_leader", to => to.GuildLeader, MappingSignificance.Optional);
            account.Map("created", to => to.Created);
            account.Map("access", to => to.Access, (in JsonElement element, in JsonPath path) => Enum.Parse<ProductName>(element.GetString(), false));
            account.Map("commander", to => to.Commander);
            account.Map("fractal_level", to => to.FractalLevel);
            account.Map("daily_ap", to => to.DailyAp);
            account.Map("monthly_ap", to => to.MonthlyAp);
            account.Map("wvw_rank", to => to.WvwRank);
            account.Map("build_storage_slots", to => to.BuildStorageSlots);
        }
    }
}
