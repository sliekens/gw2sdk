using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.MailCarriers;

/// <summary>Provides query methods for mail carriers and mail carriers unlocked on the account.</summary>
[PublicAPI]
public sealed class MailCarriersClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="MailCarriersClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public MailCarriersClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/mailcarriers

    /// <summary>Retrieves the IDs of mail carriers unlocked on the account associated with the access token. This endpoint is
    /// only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedMailCarriers(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/mailcarriers", accessToken);
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetInt32());
            return (value, response.Context);
        }
    }

    #endregion v2/account/mailcarriers

    #region v2/mailcarriers

    /// <summary>Retrieves all mail carriers.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MailCarrier> Value, MessageContext Context)> GetMailCarriers(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mailcarriers");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetMailCarrier());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all mail carriers.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetMailCarriersIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mailcarriers");
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a mail carrier by its ID.</summary>
    /// <param name="mailCarrierId">The mail carrier ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MailCarrier Value, MessageContext Context)> GetMailCarrierById(
        int mailCarrierId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mailcarriers");
        requestBuilder.Query.AddId(mailCarrierId);
        requestBuilder.Query.AddLanguage(language);
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMailCarrier();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves mail carriers by their IDs.</summary>
    /// <param name="mailCarrierIds">The mail carrier IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MailCarrier> Value, MessageContext Context)> GetMailCarriersByIds(
        IEnumerable<int> mailCarrierIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mailcarriers");
        requestBuilder.Query.AddIds(mailCarrierIds);
        requestBuilder.Query.AddLanguage(language);
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetMailCarrier());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of mail carriers.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MailCarrier> Value, MessageContext Context)> GetMailCarriersByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/mailcarriers");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await Response.Json(httpClient, request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetMailCarrier());
            return (value, response.Context);
        }
    }

    #endregion v2/mailcarriers
}
