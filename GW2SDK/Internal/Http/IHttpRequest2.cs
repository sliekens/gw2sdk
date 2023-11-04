namespace GuildWars2.Http;

internal interface IHttpRequest2<T>
{
    Task<(T Value, MessageContext Context)> SendAsync(HttpClient httpClient, CancellationToken cancellationToken);
}
