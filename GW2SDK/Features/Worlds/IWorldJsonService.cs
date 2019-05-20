using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Features.Worlds
{
    public interface IWorldJsonService
    {
        Task<(string Json, Dictionary<string, string> MetaData)> GetWorldIds();

        Task<(string Json, Dictionary<string, string> MetaData)> GetWorldById(int worldId);

        Task<(string Json, Dictionary<string, string> MetaData)> GetWorldsById(IReadOnlyList<int> worldIds);

        Task<(string Json, Dictionary<string, string> MetaData)> GetAllWorlds();

        Task<(string Json, Dictionary<string, string> MetaData)> GetWorldsByPage(int page, int? pageSize);
    }
}
