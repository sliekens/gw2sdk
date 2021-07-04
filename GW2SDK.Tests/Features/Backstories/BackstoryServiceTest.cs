﻿using System;
using System.Threading.Tasks;
using GW2SDK.Backstories;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Backstories
{
    public class BackstoryServiceTest
    {
        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_backstory_questions()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var actual = await sut.GetBackstoryQuestions();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_backstory_answers()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var actual = await sut.GetBackstoryAnswers();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_backstory_question_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var actual = await sut.GetBackstoryQuestionsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_backstory_answer_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var actual = await sut.GetBackstoryAnswersIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_backstory_question_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var questionId = 7;

            var actual = await sut.GetBackstoryQuestionById(questionId);

            Assert.Equal(questionId, actual.Value.Id);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_backstory_answer_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var answerId = "7-53";

            var actual = await sut.GetBackstoryAnswerById(answerId);

            Assert.Equal(answerId, actual.Value.Id);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Unit")]
        public async Task Backstory_question_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetBackstoryQuestionsByIds(null);
            });

            var reason = Assert.IsType<ArgumentNullException>(actual);
            Assert.Equal("questionIds", reason.ParamName);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Unit")]
        public async Task Backstory_answer_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetBackstoryAnswersByIds(null);
            });

            var reason = Assert.IsType<ArgumentNullException>(actual);
            Assert.Equal("answerIds", reason.ParamName);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Unit")]
        public async Task Backstory_question_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetBackstoryQuestionsByIds(new int[0]);
            });

            var reason = Assert.IsType<ArgumentException>(actual);
            Assert.Equal("questionIds", reason.ParamName);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Unit")]
        public async Task Backstory_answer_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetBackstoryAnswersByIds(new string[0]);
            });

            var reason = Assert.IsType<ArgumentException>(actual);
            Assert.Equal("answerIds", reason.ParamName);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Unit")]
        public async Task Backstory_answer_ids_cannot_contain_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var ids = new[]
            {
                "7-53",
                null,
                "7-55"
            };

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetBackstoryAnswersByIds(ids);
            });

            var reason = Assert.IsType<ArgumentException>(actual);
            Assert.Equal("answerIds", reason.ParamName);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Unit")]
        public async Task Backstory_answer_ids_cannot_contain_empty_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var ids = new[]
            {
                "7-53",
                "",
                "7-55"
            };

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetBackstoryAnswersByIds(ids);
            });

            var reason = Assert.IsType<ArgumentException>(actual);
            Assert.Equal("answerIds", reason.ParamName);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_backstory_questions_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var ids = new[]
            {
                7,
                10,
                11
            };

            var actual = await sut.GetBackstoryQuestionsByIds(ids);

            Assert.Collection(actual.Values,
                first => Assert.Equal(ids[0], first.Id),
                second => Assert.Equal(ids[1], second.Id),
                third => Assert.Equal(ids[2], third.Id));
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_backstory_answers_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var ids = new[]
            {
                "7-53",
                "7-54",
                "7-55"
            };

            var actual = await sut.GetBackstoryAnswersByIds(ids);

            Assert.Collection(actual.Values,
                first => Assert.Equal(ids[0], first.Id),
                second => Assert.Equal(ids[1], second.Id),
                third => Assert.Equal(ids[2], third.Id));
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_backstory_questions_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var actual = await sut.GetBackstoryQuestionsByPage(1, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }

        [Fact]
        [Trait("Feature", "Backstories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_backstory_answers_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BackstoryService>();

            var actual = await sut.GetBackstoryAnswersByPage(1, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
