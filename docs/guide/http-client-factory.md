# Http client factory

Creating ad-hoc instances of `HttpClient` is great for short-lived console apps, but not for long-running applications like server apps. Creating too many `HttpClient` instances can lead to socket exhaustion. You could create a singleton instance, but it's better to use the `IHttpClientFactory` which takes care of lifetime management of the underlying message handlers. See the full guide here: <https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests>.

You can also use `IHttpClientFactory` in a console app if you wish. It can be useful if your console app is expected to stay alive for a long time.

<<< @/samples/HttpClientFactoryUsage/Program.cs

You may also want to use Polly to become more resilient against other network errors.