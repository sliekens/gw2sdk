using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.Features.Tokens
{
    public interface ITokenInfoJsonService
    {
        Task<HttpResponseMessage> GetTokenInfo();
    }
}
