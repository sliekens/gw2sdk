using GuildWars2.Http;

namespace GuildWars2.Hero.Builds.Http;

internal sealed class SkillByIdRequest(int skillId) : IHttpRequest<Skill>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/skills")
    {
        AcceptEncoding = "gzip"
    };

    public int SkillId { get; } = skillId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Skill Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", SkillId },
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
        var value = json.RootElement.GetSkill(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
