using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Accounts.Achievements.Impl
{
    internal sealed class AccountAchievementJsonReader : JsonObjectReader<AccountAchievement>
    {
        private AccountAchievementJsonReader()
        {
            Configure(MapAccountAchievement);
        }

        public static IJsonReader<AccountAchievement> Instance { get; } = new AccountAchievementJsonReader();

        private static void MapAccountAchievement(JsonObjectMapping<AccountAchievement> accountAchievement)
        {
            accountAchievement.Map("id", to => to.Id);
            accountAchievement.Map("bits", to => to.Bits, MappingSignificance.Optional);
            accountAchievement.Map("current", to => to.Current);
            accountAchievement.Map("max", to => to.Max);
            accountAchievement.Map("done", to => to.Done);
            accountAchievement.Map("repeated", to => to.Repeated);
            accountAchievement.Map("unlocked", to => to.Unlocked);
        }
    }
}
