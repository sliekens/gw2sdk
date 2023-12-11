using GuildWars2.Http;

namespace GuildWars2.Hero.Builds.Http;

internal sealed class SpecializationByIdRequest(int specializationId) : IHttpRequest<Specialization>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/specializations")
    {
        AcceptEncoding = "gzip"
    };

    public int SpecializationId { get; } = specializationId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Specialization Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", SpecializationId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSpecialization(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
