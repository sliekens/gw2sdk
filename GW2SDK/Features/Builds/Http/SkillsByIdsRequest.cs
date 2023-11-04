using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Builds.Http;

internal sealed class SkillsByIdsRequest : IHttpRequest<HashSet<Skill>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/skills")
    {
        AcceptEncoding = "gzip"
    };

    public SkillsByIdsRequest(IReadOnlyCollection<int> skillIds)
    {
        Check.Collection(skillIds);
        SkillIds = skillIds;
    }

    public IReadOnlyCollection<int> SkillIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Skill> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", SkillIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetSkill(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
