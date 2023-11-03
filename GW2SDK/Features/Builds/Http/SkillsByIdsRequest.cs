using GuildWars2.Builds.Skills;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Builds.Http;

internal sealed class SkillsByIdsRequest : IHttpRequest<Replica<HashSet<Skill>>>
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

    public async Task<Replica<HashSet<Skill>>> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<Skill>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetSkill(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
