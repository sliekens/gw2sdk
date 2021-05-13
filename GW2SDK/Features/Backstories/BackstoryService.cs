using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
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

        public async Task<IDataTransferCollection<BackstoryQuestion>> GetBackstoryQuestions()
        {
            var request = new BackstoryQuestionsRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<BackstoryQuestion>(context.ResultCount);
            list.AddRange(_backstoryReader.Question.ReadArray(json));
            return new DataTransferCollection<BackstoryQuestion>(list, context);
        }

        public async Task<IDataTransferCollection<BackstoryAnswer>> GetBackstoryAnswers()
        {
            var request = new BackstoryAnswersRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<BackstoryAnswer>(context.ResultCount);
            list.AddRange(_backstoryReader.Answer.ReadArray(json));
            return new DataTransferCollection<BackstoryAnswer>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetBackstoryQuestionsIndex()
        {
            var request = new BackstoryQuestionsIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_backstoryReader.Question.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<IDataTransferCollection<string>> GetBackstoryAnswersIndex()
        {
            var request = new BackstoryAnswersIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<string>(context.ResultCount);
            list.AddRange(_backstoryReader.Answer.Id.ReadArray(json));
            return new DataTransferCollection<string>(list, context);
        }

        public async Task<BackstoryQuestion?> GetBackstoryQuestionById(int questionId)
        {
            var request = new BackstoryQuestionByIdRequest(questionId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _backstoryReader.Question.Read(json);
        }

        public async Task<BackstoryAnswer?> GetBackstoryAnswerById(string answerId)
        {
            var request = new BackstoryAnswerByIdRequest(answerId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _backstoryReader.Answer.Read(json);
        }

        public async Task<IDataTransferCollection<BackstoryQuestion>> GetBackstoryQuestionsByIds(IReadOnlyCollection<int> questionIds)
        {
            if (questionIds is null)
            {
                throw new ArgumentNullException(nameof(questionIds));
            }

            if (questionIds.Count == 0)
            {
                throw new ArgumentException("Backstory question IDs cannot be an empty collection.", nameof(questionIds));
            }

            var request = new BackstoryQuestionsByIdsRequest(questionIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<BackstoryQuestion>(context.ResultCount);
            list.AddRange(_backstoryReader.Question.ReadArray(json));
            return new DataTransferCollection<BackstoryQuestion>(list, context);
        }

        public async Task<IDataTransferCollection<BackstoryAnswer>> GetBackstoryAnswersByIds(IReadOnlyCollection<string> answerIds)
        {
            if (answerIds is null)
            {
                throw new ArgumentNullException(nameof(answerIds));
            }

            if (answerIds.Count == 0)
            {
                throw new ArgumentException("Backstory answer IDs cannot be an empty collection.", nameof(answerIds));
            }

            if (answerIds.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentException("Backstory answer IDs collection cannot contain empty values.", nameof(answerIds));
            }

            var request = new BackstoryAnswersByIdsRequest(answerIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<BackstoryAnswer>(context.ResultCount);
            list.AddRange(_backstoryReader.Answer.ReadArray(json));
            return new DataTransferCollection<BackstoryAnswer>(list, context);
        }

        public async Task<IDataTransferPage<BackstoryQuestion>> GetBackstoryQuestionsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new BackstoryQuestionsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<BackstoryQuestion>(pageContext.PageSize);
            list.AddRange(_backstoryReader.Question.ReadArray(json));
            return new DataTransferPage<BackstoryQuestion>(list, pageContext);
        }

        public async Task<IDataTransferPage<BackstoryAnswer>> GetBackstoryAnswersByPage(int pageIndex, int? pageSize = null)
        {
            var request = new BackstoryAnswersByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<BackstoryAnswer>(pageContext.PageSize);
            list.AddRange(_backstoryReader.Answer.ReadArray(json));
            return new DataTransferPage<BackstoryAnswer>(list, pageContext);
        }
    }
}
