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

namespace GW2SDK.Backstories
{
    [PublicAPI]
    public sealed class BackstoryService
    {
        private readonly HttpClient _http;

        private readonly IBackstoryReader _backstoryReader;

        public BackstoryService(HttpClient http, IBackstoryReader backstoryReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _backstoryReader = backstoryReader ?? throw new ArgumentNullException(nameof(backstoryReader));
        }

        public async Task<IDataTransferSet<BackstoryQuestion>> GetBackstoryQuestions()
        {
            var request = new BackstoryQuestionsRequest();
            return await _http.GetResourcesSet(request, json => _backstoryReader.Question.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<BackstoryAnswer>> GetBackstoryAnswers()
        {
            var request = new BackstoryAnswersRequest();
            return await _http.GetResourcesSet(request, json => _backstoryReader.Answer.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetBackstoryQuestionsIndex()
        {
            var request = new BackstoryQuestionsIndexRequest();
            return await _http.GetResourcesSet(request, json => _backstoryReader.Question.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<string>> GetBackstoryAnswersIndex()
        {
            var request = new BackstoryAnswersIndexRequest();
            return await _http.GetResourcesSet(request, json => _backstoryReader.Answer.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<BackstoryQuestion> GetBackstoryQuestionById(int questionId)
        {
            var request = new BackstoryQuestionByIdRequest(questionId);
            return await _http.GetResource(request, json => _backstoryReader.Question.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<BackstoryAnswer> GetBackstoryAnswerById(string answerId)
        {
            var request = new BackstoryAnswerByIdRequest(answerId);
            return await _http.GetResource(request, json => _backstoryReader.Answer.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<BackstoryQuestion>> GetBackstoryQuestionsByIds(IReadOnlyCollection<int> questionIds)
        {
            var request = new BackstoryQuestionsByIdsRequest(questionIds);
            return await _http.GetResourcesSet(request, json => _backstoryReader.Question.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<BackstoryAnswer>> GetBackstoryAnswersByIds(IReadOnlyCollection<string> answerIds)
        {
            var request = new BackstoryAnswersByIdsRequest(answerIds);
            return await _http.GetResourcesSet(request, json => _backstoryReader.Answer.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<BackstoryQuestion>> GetBackstoryQuestionsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new BackstoryQuestionsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _backstoryReader.Question.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<BackstoryAnswer>> GetBackstoryAnswersByPage(int pageIndex, int? pageSize = null)
        {
            var request = new BackstoryAnswersByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _backstoryReader.Answer.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
