using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Features.Accounts
{
    public interface IAccountJsonService
    {
        Task<(string Json, Dictionary<string, string> MetaData)> GetAccount();
    }
}