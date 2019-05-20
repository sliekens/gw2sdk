using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.Features.Accounts
{
    public interface IAccountJsonService
    {
        Task<HttpResponseMessage> GetAccount();
    }
}
