using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Features.Tokens.Infrastructure
{
    public interface IJsonTokenInfoService
    {
        Task<(string Json, Dictionary<string, string> MetaData)> GetTokenInfo();
    }
}
