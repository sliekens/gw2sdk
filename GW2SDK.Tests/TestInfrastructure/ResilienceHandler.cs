using Polly;

namespace GuildWars2.Tests.TestInfrastructure
{
    public class ResilienceHandler : DelegatingHandler
    {
        private readonly ResiliencePipeline<HttpResponseMessage> resiliencePipeline =
            new ResiliencePipelineBuilder<HttpResponseMessage>()
                .AddRetry(Gw2Resiliency.RetryStrategy)
                .AddHedging(Gw2Resiliency.HedgingStrategy)
                .Build();

        public ResilienceHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

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
}
