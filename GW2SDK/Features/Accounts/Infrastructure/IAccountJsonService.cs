using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Features.Accounts.Infrastructure
{
    public interface IAccountJsonService
    {
        Task<(string Json, Dictionary<string, string> MetaData)> GetAccount();
    }
}