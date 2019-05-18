using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Features.Common;

namespace GW2SDK.Features.Worlds.Infrastructure
{
    public interface IJsonWorldService
    {
        Task<(string Json, ListMetaData MetaData)> GetAllWorlds();

        Task<(string Json, ListMetaData MetaData)> GetWorldIds();

        Task<string> GetWorldById(int worldId);

        Task<(string Json, ListMetaData MetaData)> GetWorldsById(IReadOnlyList<int> worldIds);
    }
}
