using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Quests;

[PublicAPI]
public static class QuestJson
{
    public static Quest GetQuest(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<int> level = new("level");
        RequiredMember<int> story = new("story");
        RequiredMember<Goal> goals = new("goals");
        RequiredMember<int> id = new("id");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (member.NameEquals(story.Name))
            {
                story.Value = member.Value;
            }
            else if (member.NameEquals(goals.Name))
            {
                goals.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Quest
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Level = level.GetValue(),
            Story = story.GetValue(),
            Goals = goals.SelectMany(value => value.GetGoal(missingMemberBehavior))
        };
    }
}
