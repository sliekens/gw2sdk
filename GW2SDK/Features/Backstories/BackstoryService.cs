﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Backstories.Answers;
using GW2SDK.Backstories.Answers.Http;
using GW2SDK.Backstories.Answers.Json;
using GW2SDK.Backstories.Questions;
using GW2SDK.Backstories.Questions.Http;
using GW2SDK.Backstories.Questions.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Backstories
{
    [PublicAPI]
    public sealed class BackstoryService
    {
        private readonly HttpClient http;

        public BackstoryService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<BackstoryQuestion>> GetBackstoryQuestions(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new BackstoryQuestionsRequest(language);
            return await http.GetResourcesSet(request,
                    json =>
                        json.RootElement.GetArray(item => BackstoryQuestionReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<BackstoryAnswer>> GetBackstoryAnswers(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new BackstoryAnswersRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => BackstoryAnswerReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        #region /v2/backstory/questions

        public async Task<IReplicaSet<int>> GetBackstoryQuestionsIndex(CancellationToken cancellationToken = default)
        {
            var request = new BackstoryQuestionsIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<BackstoryQuestion>> GetBackstoryQuestionById(
            int questionId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new BackstoryQuestionByIdRequest(questionId, language);
            return await http.GetResource(request,
                    json => BackstoryQuestionReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<BackstoryQuestion>> GetBackstoryQuestionsByIds(
#if NET
            IReadOnlySet<int> questionIds,
#else
            IReadOnlyCollection<int> questionIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new BackstoryQuestionsByIdsRequest(questionIds, language);
            return await http.GetResourcesSet(request,
                    json =>
                        json.RootElement.GetArray(item => BackstoryQuestionReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<BackstoryQuestion>> GetBackstoryQuestionsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new BackstoryQuestionsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json =>
                        json.RootElement.GetArray(item => BackstoryQuestionReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion

        #region /v2/backstory/answers

        public async Task<IReplicaSet<string>> GetBackstoryAnswersIndex(CancellationToken cancellationToken = default)
        {
            var request = new BackstoryAnswersIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetStringArray(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<BackstoryAnswer>> GetBackstoryAnswerById(
            string answerId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new BackstoryAnswerByIdRequest(answerId, language);
            return await http.GetResource(request,
                    json => BackstoryAnswerReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<BackstoryAnswer>> GetBackstoryAnswersByIds(
#if NET
            IReadOnlySet<string> answerIds,
#else
            IReadOnlyCollection<string> answerIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new BackstoryAnswersByIdsRequest(answerIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => BackstoryAnswerReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<BackstoryAnswer>> GetBackstoryAnswersByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new BackstoryAnswersByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => BackstoryAnswerReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
