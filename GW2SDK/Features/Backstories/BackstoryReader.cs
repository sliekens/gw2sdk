using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Backstories.Answers;
using GW2SDK.Backstories.Questions;
using GW2SDK.Json;

namespace GW2SDK.Backstories
{
    [PublicAPI]
    public sealed class BackstoryReader : IBackstoryReader,
        IBackstoryQuestionReader,
        IBackstoryAnswerReader
    {
        public IBackstoryQuestionReader Question => this;

        public IBackstoryAnswerReader Answer => this;

        BackstoryQuestion IJsonReader<BackstoryQuestion>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var title = new RequiredMember<string>("title");
            var description = new RequiredMember<string>("description");
            var answers = new RequiredMember<string[]>("answers");
            var order = new RequiredMember<int>("order");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var races = new OptionalMember<Race[]>("races");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(title.Name))
                {
                    title = title.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(answers.Name))
                {
                    answers = answers.From(member.Value);
                }
                else if (member.NameEquals(order.Name))
                {
                    order = order.From(member.Value);
                }
                else if (member.NameEquals(professions.Name))
                {
                    professions = professions.From(member.Value);
                }
                else if (member.NameEquals(races.Name))
                {
                    races = races.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new BackstoryQuestion
            {
                Id = id.GetValue(),
                Title = title.GetValue(),
                Description = description.GetValue(),
                Answers = answers.Select(value => value.GetArray(item => item.GetStringRequired())),
                Order = order.GetValue(),
                Professions = professions.GetValue(missingMemberBehavior),
                Races = races.GetValue(missingMemberBehavior)
            };
        }

        IJsonReader<int> IBackstoryQuestionReader.Id => new Int32JsonReader();

        BackstoryAnswer IJsonReader<BackstoryAnswer>.Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<string>("id");
            var title = new RequiredMember<string>("title");
            var description = new RequiredMember<string>("description");
            var journal = new RequiredMember<string>("journal");
            var question = new RequiredMember<int>("question");
            var professions = new OptionalMember<ProfessionName[]>("professions");
            var races = new OptionalMember<Race[]>("races");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(title.Name))
                {
                    title = title.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(journal.Name))
                {
                    journal = journal.From(member.Value);
                }
                else if (member.NameEquals(question.Name))
                {
                    question = question.From(member.Value);
                }
                else if (member.NameEquals(professions.Name))
                {
                    professions = professions.From(member.Value);
                }
                else if (member.NameEquals(races.Name))
                {
                    races = races.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new BackstoryAnswer
            {
                Id = id.GetValue(),
                Title = title.GetValue(),
                Description = description.GetValue(),
                Journal = journal.GetValue(),
                Question = question.GetValue(),
                Professions = professions.GetValue(missingMemberBehavior),
                Races = races.GetValue(missingMemberBehavior)
            };
        }

        IJsonReader<string> IBackstoryAnswerReader.Id => new StringJsonReader();
    }
}