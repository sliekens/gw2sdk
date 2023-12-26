using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class StoryStepJson
{
    public static StoryStep GetStoryStep(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember level = "level";
        RequiredMember story = "story";
        RequiredMember goals = "goals";
        RequiredMember id = "id";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == level.Name)
            {
                level = member;
            }
            else if (member.Name == story.Name)
            {
                story = member;
            }
            else if (member.Name == goals.Name)
            {
                goals = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new StoryStep
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Level = level.Map(value => value.GetInt32()),
            StoryId = story.Map(value => value.GetInt32()),
            Objectives = goals.Map(
                values => values.GetList(value => value.GetObjective(missingMemberBehavior))
            )
        };
    }
}
