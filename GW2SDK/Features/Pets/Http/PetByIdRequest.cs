﻿using GuildWars2.Http;

namespace GuildWars2.Pets.Http;

internal sealed class PetByIdRequest : IHttpRequest2<Pet>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pets") { AcceptEncoding = "gzip" };

    public PetByIdRequest(int petId)
    {
        PetId = petId;
    }

    public int PetId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetPet(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
