using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class StoryStepJson
{
    public static StoryStep GetStoryStep(this JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember level = "level";
        RequiredMember story = "story";
        RequiredMember goals = "goals";
        RequiredMember id = "id";

        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (level.Match(member))
            {
                level = member;
            }
            else if (story.Match(member))
            {
                story = member;
            }
            else if (goals.Match(member))
            {
                goals = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new StoryStep
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Level = level.Map(static value => value.GetInt32()),
            StoryId = story.Map(static value => value.GetInt32()),
            Objectives =
                goals.Map(static values => values.GetList(static value => value.GetObjective()))
        };
    }
}
