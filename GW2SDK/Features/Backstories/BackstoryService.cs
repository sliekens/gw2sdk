using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Backstories.Answers;
using GW2SDK.Backstories.Answers.Http;
using GW2SDK.Backstories.Questions;
using GW2SDK.Backstories.Questions.Http;
using GW2SDK.Http;
using GW2SDK.Json;

namespace GW2SDK.Backstories
{
    [PublicAPI]
    public sealed class BackstoryService
    {
        private readonly HttpClient _http;

        private readonly IBackstoryReader _backstoryReader;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public BackstoryService(HttpClient http, IBackstoryReader backstoryReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _backstoryReader = backstoryReader ?? throw new ArgumentNullException(nameof(backstoryReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<BackstoryQuestion>> GetBackstoryQuestions()
        {
            var request = new BackstoryQuestionsRequest();
            return await _http.GetResourcesSet(request, json => _backstoryReader.Question.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<BackstoryAnswer>> GetBackstoryAnswers()
        {
            var request = new BackstoryAnswersRequest();
            return await _http.GetResourcesSet(request, json => _backstoryReader.Answer.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetBackstoryQuestionsIndex()
        {
            var request = new BackstoryQuestionsIndexRequest();
            return await _http.GetResourcesSet(request, json => _backstoryReader.Question.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<string>> GetBackstoryAnswersIndex()
        {
            var request = new BackstoryAnswersIndexRequest();
            return await _http.GetResourcesSet(request, json => _backstoryReader.Answer.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<BackstoryQuestion>> GetBackstoryQuestionById(int questionId)
        {
            var request = new BackstoryQuestionByIdRequest(questionId);
            return await _http.GetResource(request, json => _backstoryReader.Question.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<BackstoryAnswer>> GetBackstoryAnswerById(string answerId)
        {
            var request = new BackstoryAnswerByIdRequest(answerId);
            return await _http.GetResource(request, json => _backstoryReader.Answer.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<BackstoryQuestion>> GetBackstoryQuestionsByIds(IReadOnlyCollection<int> questionIds)
        {
            var request = new BackstoryQuestionsByIdsRequest(questionIds);
            return await _http.GetResourcesSet(request, json => _backstoryReader.Question.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<BackstoryAnswer>> GetBackstoryAnswersByIds(IReadOnlyCollection<string> answerIds)
        {
            var request = new BackstoryAnswersByIdsRequest(answerIds);
            return await _http.GetResourcesSet(request, json => _backstoryReader.Answer.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<BackstoryQuestion>> GetBackstoryQuestionsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new BackstoryQuestionsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _backstoryReader.Question.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<BackstoryAnswer>> GetBackstoryAnswersByPage(int pageIndex, int? pageSize = null)
        {
            var request = new BackstoryAnswersByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _backstoryReader.Answer.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
