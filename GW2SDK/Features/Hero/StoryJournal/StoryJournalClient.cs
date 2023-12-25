using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Hero.StoryJournal.Http;
using GuildWars2.Hero.StoryJournal.Stories;

namespace GuildWars2.Hero.StoryJournal;

/// <summary>Provides query methods for the story progress and the backstory of a character.</summary>
[PublicAPI]
public sealed class StoryJournalClient
{
    private readonly HttpClient httpClient;

    public StoryJournalClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/characters/:id/backstory

    /// <summary>Retrieves the background story of a character on the account associated with the access token. This endpoint
    /// is only accessible with a valid access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(CharacterBackgroundStory Value, MessageContext Context)>
        GetCharacterBackgroundStory(
            string characterName,
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        CharacterBackstoryRequest request = new(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/characters/:id/quests

    public Task<(HashSet<int> Value, MessageContext Context)> GetCharacterQuests(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CharacterQuestsRequest request = new(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/characters/:id/quests

    #region v2/backstory/questions

    /// <summary>Retrieves all background story questions.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<BackgroundStoryQuestion> Value, MessageContext Context)>
        GetBackgroundStoryQuestions(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        BackstoryQuestionRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all background story questions.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetBackgroundStoryQuestionsIndex(
        CancellationToken cancellationToken = default
    )
    {
        BackstoryQuestionsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a background story question by its ID.</summary>
    /// <param name="questionId">the background story question ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(BackgroundStoryQuestion Value, MessageContext Context)>
        GetBackgroundStoryQuestionById(
            int questionId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        BackstoryQuestionByIdRequest request = new(questionId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves background story questions by their IDs.</summary>
    /// <param name="questionIds">the background story question IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<BackgroundStoryQuestion> Value, MessageContext Context)>
        GetBackgroundStoryQuestionsByIds(
            IReadOnlyCollection<int> questionIds,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        BackstoryQuestionsByIdsRequest request = new(questionIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of background story questions.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<BackgroundStoryQuestion> Value, MessageContext Context)>
        GetBackgroundStoryQuestionsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        BackstoryQuestionsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/backstory/answers

    /// <summary>Retrieves all background story answers.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<BackgroundStoryAnswer> Value, MessageContext Context)>
        GetBackgroundStoryAnswers(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        BackstoryAnswersRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all background story answers.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetBackgroundStoryAnswersIndex(
        CancellationToken cancellationToken = default
    )
    {
        BackstoryAnswersIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a background story answer by its ID.</summary>
    /// <param name="answerId">The background story answer ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(BackgroundStoryAnswer Value, MessageContext Context)> GetBackgroundStoryAnswerById(
        string answerId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BackstoryAnswerByIdRequest request = new(answerId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves background story answers by their IDs.</summary>
    /// <param name="answerIds">The background story answer IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<BackgroundStoryAnswer> Value, MessageContext Context)>
        GetBackgroundStoryAnswersByIds(
            IReadOnlyCollection<string> answerIds,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        BackstoryAnswersByIdsRequest request = new(answerIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of background story answers.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<BackgroundStoryAnswer> Value, MessageContext Context)>
        GetBackgroundStoryAnswersByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        BackstoryAnswersByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/stories

    public Task<(HashSet<Story> Value, MessageContext Context)> GetStories(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        StoriesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetStoriesIndex(
        CancellationToken cancellationToken = default
    )
    {
        StoriesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Story Value, MessageContext Context)> GetStoryById(
        int storyId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        StoryByIdRequest request = new(storyId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Story> Value, MessageContext Context)> GetStoriesByIds(
        IReadOnlyCollection<int> storyIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        StoriesByIdsRequest request = new(storyIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Story> Value, MessageContext Context)> GetStoriesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        StoriesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/stories/seasons

    public Task<(HashSet<Season> Value, MessageContext Context)> GetSeasons(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SeasonsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetSeasonsIndex(
        CancellationToken cancellationToken = default
    )
    {
        SeasonsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Season Value, MessageContext Context)> GetSeasonById(
        string seasonId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SeasonByIdRequest request = new(seasonId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Season> Value, MessageContext Context)> GetSeasonsByIds(
        IReadOnlyCollection<string> seasonIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SeasonsByIdsRequest request = new(seasonIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Season> Value, MessageContext Context)> GetSeasonsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SeasonsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/quests

    public Task<(HashSet<Quest> Value, MessageContext Context)> GetQuests(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuestsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetQuestsIndex(
        CancellationToken cancellationToken = default
    )
    {
        QuestsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Quest Value, MessageContext Context)> GetQuestById(
        int questId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuestByIdRequest request = new(questId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Quest> Value, MessageContext Context)> GetQuestsByIds(
        IReadOnlyCollection<int> questIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuestsByIdsRequest request = new(questIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Quest> Value, MessageContext Context)> GetQuestsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuestsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/quests
}
