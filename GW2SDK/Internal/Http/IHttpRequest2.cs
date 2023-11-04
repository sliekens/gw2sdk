namespace GuildWars2.Http;

internal interface IHttpRequest2<T>
{
    Task<(T, MessageContext)> SendAsync(HttpClient httpClient, CancellationToken cancellationToken);
}
