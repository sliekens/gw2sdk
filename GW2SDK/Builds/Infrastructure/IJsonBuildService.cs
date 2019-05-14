using System.Threading.Tasks;

namespace GW2SDK.Builds.Infrastructure
{
    public interface IJsonBuildService
    {
        Task<string> GetBuildAsync();
    }
}