using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Backstories.Answers;
using GW2SDK.Backstories.Answers.Http;
using GW2SDK.Backstories.Questions;
using GW2SDK.Backstories.Questions.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Backstories
{
    [PublicAPI]
    public sealed class BackstoryService
    {
        private readonly IBackstoryReader _backstoryReader;

        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public BackstoryService(
            HttpClient http,
            IBackstoryReader backstoryReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _backstoryReader = backstoryReader ?? throw new ArgumentNullException(nameof(backstoryReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<BackstoryQuestion>> GetBackstoryQuestions(Language? language = default)
        {
            var request = new BackstoryQuestionsRequest(language);
            return await _http.GetResourcesSet(request,
                    json => _backstoryReader.Question.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<BackstoryAnswer>> GetBackstoryAnswers(Language? language = default)
        {
            var request = new BackstoryAnswersRequest(language);
            return await _http.GetResourcesSet(request,
                    json => _backstoryReader.Answer.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetBackstoryQuestionsIndex()
        {
            var request = new BackstoryQuestionsIndexRequest();
            return await _http.GetResourcesSet(request,
                    json => _backstoryReader.Question.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<string>> GetBackstoryAnswersIndex()
        {
            var request = new BackstoryAnswersIndexRequest();
            return await _http.GetResourcesSet(request,
                    json => _backstoryReader.Answer.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<BackstoryQuestion>> GetBackstoryQuestionById(
            int questionId,
            Language? language = default
        )
        {
            var request = new BackstoryQuestionByIdRequest(questionId, language);
            return await _http
                .GetResource(request, json => _backstoryReader.Question.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<BackstoryAnswer>> GetBackstoryAnswerById(
            string answerId,
            Language? language = default
        )
        {
            var request = new BackstoryAnswerByIdRequest(answerId, language);
            return await _http.GetResource(request, json => _backstoryReader.Answer.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<BackstoryQuestion>> GetBackstoryQuestionsByIds(
            IReadOnlyCollection<int> questionIds,
            Language? language = default
        )
        {
            var request = new BackstoryQuestionsByIdsRequest(questionIds, language);
            return await _http.GetResourcesSet(request,
                    json => _backstoryReader.Question.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<BackstoryAnswer>> GetBackstoryAnswersByIds(
            IReadOnlyCollection<string> answerIds,
            Language? language = default
        )
        {
            var request = new BackstoryAnswersByIdsRequest(answerIds, language);
            return await _http.GetResourcesSet(request,
                    json => _backstoryReader.Answer.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<BackstoryQuestion>> GetBackstoryQuestionsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new BackstoryQuestionsByPageRequest(pageIndex, pageSize, language);
            return await _http.GetResourcesPage(request,
                    json => _backstoryReader.Question.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<BackstoryAnswer>> GetBackstoryAnswersByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new BackstoryAnswersByPageRequest(pageIndex, pageSize, language);
            return await _http.GetResourcesPage(request,
                    json => _backstoryReader.Answer.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
