using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Accounts.Achievements
{
    [PublicAPI]
    public interface IAccountAchievementReader : IJsonReader<AccountAchievement>
    {
    }
}
