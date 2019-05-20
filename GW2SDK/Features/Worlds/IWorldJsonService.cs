using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.Features.Worlds
{
    public interface IWorldJsonService
    {
        Task<HttpResponseMessage> GetWorldIds();

        Task<HttpResponseMessage> GetWorldById(int worldId);

        Task<HttpResponseMessage> GetWorldsById(IReadOnlyList<int> worldIds);

        Task<HttpResponseMessage> GetAllWorlds();

        Task<HttpResponseMessage> GetWorldsByPage(int page, int? pageSize);
    }
}
