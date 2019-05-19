using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Features.Accounts.Infrastructure
{
    public interface IJsonAccountsService
    {
        Task<(string Json, Dictionary<string, string> MetaData)> GetAccount();
    }
}