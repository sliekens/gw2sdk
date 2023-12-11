using Polly;

namespace GuildWars2.Tests.TestInfrastructure;

public class ResilienceHandler(HttpMessageHandler innerHandler) : DelegatingHandler(innerHandler)
{
    private readonly ResiliencePipeline<HttpResponseMessage> resiliencePipeline =
        new ResiliencePipelineBuilder<HttpResponseMessage>().AddRetry(Gw2Resiliency.RetryStrategy)
            .AddHedging(Gw2Resiliency.HedgingStrategy)
            .Build();

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    ) =>
        await resiliencePipeline.ExecuteAsync(
                async cancellationToken =>
                    await base.SendAsync(request, cancellationToken).ConfigureAwait(false),
                cancellationToken
            )
            .ConfigureAwait(false);
}
