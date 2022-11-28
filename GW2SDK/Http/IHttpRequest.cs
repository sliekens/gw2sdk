using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GuildWars2.Http;

[PublicAPI]
public interface IHttpRequest<T>
{
    Task<T> SendAsync(HttpClient httpClient, CancellationToken cancellationToken);
}
