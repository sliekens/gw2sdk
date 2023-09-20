namespace GuildWars2.Http;

[PublicAPI]
public interface IHttpRequest<T>
{
    Task<T> SendAsync(HttpClient httpClient, CancellationToken cancellationToken);
}
