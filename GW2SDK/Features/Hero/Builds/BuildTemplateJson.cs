using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

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
            if (tab.Match(member))
            {
                tab = member;
            }
            else if (build.Match(member))
            {
                build = member;
            }
            else if (isActive.Match(member))
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
