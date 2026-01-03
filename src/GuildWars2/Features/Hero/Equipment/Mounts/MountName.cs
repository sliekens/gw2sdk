using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Mounts;

/// <summary>The mount types.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(MountNameJsonConverter))]
public enum MountName
{
    /// <summary>No specific mount or unknown mount.</summary>
    None,

    /// <summary>Swift, strong, and agile, raptors are stalwart explorers and great companions for exploring the far reaches of
    /// the Crystal Desert.</summary>
    Raptor,

    /// <summary>Springers are bounding beasts whose powerful legs easily maneuver them through the craggy landscapes of the
    /// Desert Highlands and beyond.</summary>
    Springer,

    /// <summary>The graceful movements of the skimmer allow it to float above land and water, which is especially useful on
    /// the Elon River and other hazardous parts of the desert.</summary>
    Skimmer,

    /// <summary>A magical construct formed by fusing the unbound energy of the Forgotten with the very stones and sand of the
    /// Crystal Desert.</summary>
    Jackal,

    /// <summary>A once-humble scarab beetle, bulked up to phenomenal size and strength by asuran science.</summary>
    RollerBeetle,

    /// <summary>Glorious and noble beasts, the art of taming griffons for flight has only been recently recovered.</summary>
    Griffon,

    /// <summary>Trained for battle and siege warfare, the warclaw knows no fear.</summary>
    Warclaw,

    /// <summary>Thanks to your training, your skyscale now has an instinctive bond with you and a desire to soar to new
    /// heights across Tyria.</summary>
    Skyscale,

    /// <summary>Your personal skiff is your home away from home on the waves. Explore, relax, and ferry your whole party! Drop
    /// anchor to fish, walk around, and more.</summary>
    Skiff,

    /// <summary>The Luxon Armada’s descendants have raised and trained these massive turtles for over two hundred years. Each
    /// mount can bear two riders: one to handle the Turtle, and another to operate the weapons strapped to its shell. Raise
    /// your own walking war machine and take a friend out for combat adventures!</summary>
    SiegeTurtle
}
