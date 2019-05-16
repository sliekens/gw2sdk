using System.Threading.Tasks;

namespace GW2SDK.Features.Tokens.Infrastructure
{
    public interface IJsonTokenInfoService
    {
        Task<string> GetTokenInfo();
    }
}
