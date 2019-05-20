using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.Features.Builds
{
    public interface IBuildJsonService
    {
        Task<HttpResponseMessage> GetBuild();
    }
}
