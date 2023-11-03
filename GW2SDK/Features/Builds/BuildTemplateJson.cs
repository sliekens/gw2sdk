using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Builds;

internal static class BuildTemplateJson
{
    public static BuildTemplate GetBuildTemplate(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember tab = "tab";
        RequiredMember isActive = "is_active";
        RequiredMember build = "build";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == tab.Name)
            {
                tab = member;
            }
            else if (member.Name == build.Name)
            {
                build = member;
            }
            else if (member.Name == isActive.Name)
            {
                isActive = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuildTemplate
        {
            TabNumber = tab.Map(value => value.GetInt32()),
            IsActive = isActive.Map(value => value.GetBoolean()),
            Build = build.Map(value => value.GetBuild(missingMemberBehavior))
        };
    }
}
