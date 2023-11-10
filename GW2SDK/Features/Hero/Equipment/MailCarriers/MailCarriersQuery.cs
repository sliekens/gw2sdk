using GuildWars2.Hero.Equipment.MailCarriers.Http;

namespace GuildWars2.Hero.Equipment.MailCarriers;

[PublicAPI]
public sealed class MailCarriersQuery
{
    private readonly HttpClient http;

    public MailCarriersQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<MailCarrier> Value, MessageContext Context)> GetMailCarriers(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetMailCarriersIndex(
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(MailCarrier Value, MessageContext Context)> GetMailCarrierById(
        int mailCarrierId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarrierByIdRequest request = new(mailCarrierId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<MailCarrier> Value, MessageContext Context)> GetMailCarriersByIds(
        IReadOnlyCollection<int> mailCarrierIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersByIdsRequest request = new(mailCarrierIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<MailCarrier> Value, MessageContext Context)> GetMailCarriersByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetOwnedMailCarriers(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        OwnedMailCarriersRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }
}
