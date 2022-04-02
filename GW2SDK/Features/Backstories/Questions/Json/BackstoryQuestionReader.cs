using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Backstories.Questions.Json
{
    [PublicAPI]
    public static class BackstoryQuestionReader
    {
        public static BackstoryQuestion Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var title = new RequiredMember<string>("title");
            var description = new RequiredMember<string>("description");
            var answers = new RequiredMember<string>("answers");
            var order = new RequiredMember<int>("order");
            var professions = new OptionalMember<ProfessionName>("professions");
            var races = new OptionalMember<Race>("races");
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
                Answers = answers.SelectMany(value => value.GetStringRequired()),
                Order = order.GetValue(),
                Professions = professions.GetValues(missingMemberBehavior),
                Races = races.GetValues(missingMemberBehavior)
            };
        }
    }
}
