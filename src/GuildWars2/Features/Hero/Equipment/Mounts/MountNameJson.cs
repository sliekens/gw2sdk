using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts;

internal static class MountNameJson
{
    public static Extensible<MountName> GetMountName(this in JsonElement json)
    {
        string text = json.GetStringRequired();
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
            "turtle" => MountName.SiegeTurtle,
            _ => new Extensible<MountName>(text)
        };
    }
}
