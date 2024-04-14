using GuildWars2.Http;

namespace GuildWars2.Pve.Pets.Http;

internal sealed class PetByIdRequest(int petId) : IHttpRequest<Pet>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pets") { AcceptEncoding = "gzip" };

    public int PetId { get; } = petId;

    public Language? Language { get; init; }

    
    public async Task<(Pet Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", PetId },
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
        var value = json.RootElement.GetPet();
        return (value, new MessageContext(response));
    }
}
