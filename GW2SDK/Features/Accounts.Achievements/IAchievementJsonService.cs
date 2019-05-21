using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Tokens;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Accounts.Achievements
{
    public interface IAchievementJsonService
    {
        [Scope(Permission.Progression)]
        Task<HttpResponseMessage> GetAchievements();
    }
}
