using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Mounts;

[PublicAPI]
public static class MountNameJson
{
    public static MountName GetMountName(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var text = json.GetStringRequired();
        return text switch
        {
            "griffon" => MountName.Griffon,
            "jackal" => MountName.Jackal,
            "raptor" => MountName.Raptor,
            "roller_beetle" => MountName.RollerBeetle,
            "skimmer" => MountName.Skimmer,
            "skyscale" => MountName.Skyscale,
            "springer" => MountName.Springer,
            "warclaw" => MountName.Warclaw,
            "turtle" => MountName.Turtle,
            _ when missingMemberBehavior is MissingMemberBehavior.Error =>
                throw new InvalidOperationException(Strings.UnexpectedMember(text)),
            _ => (MountName)text.GetDeterministicHashCode()
        };
    }
}
