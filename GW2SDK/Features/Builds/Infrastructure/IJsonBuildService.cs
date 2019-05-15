using System.Threading.Tasks;

namespace GW2SDK.Features.Builds.Infrastructure
{
    public interface IJsonBuildService
    {
        Task<string> GetBuildAsync();
    }
}