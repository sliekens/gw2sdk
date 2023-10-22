namespace GuildWars2.Http;

internal interface IHttpRequest<T>
{
    Task<T> SendAsync(HttpClient httpClient, CancellationToken cancellationToken);
}
