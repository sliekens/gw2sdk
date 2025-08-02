using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero;

/// <summary>The playable races.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(RaceNameJsonConverter))]
public enum RaceName
{
    /// <summary>No specific race or unknown race.</summary>
    None,

    /// <summary>These alchemagical inventors may be short in stature, but they're intellectual giants. Among the asura, it's
    /// not the strong who survive, but the clever. Other races believe they should rule by virtue of their power and strength,
    /// but they're deluding themselves. In due time, all will serve the asura.</summary>
    Asura,

    /// <summary>The charr race was forged in the merciless crucible of war. It is all they know. War defines them, and their
    /// quest for dominion drives them ever onward. The weakling and the fool have no place among the charr. Victory is all
    /// that matters, and it must be achieved by any means and at any cost.</summary>
    Charr,

    /// <summary>Humans have lost their homeland, their security, and their former glory. Even their gods have withdrawn. And
    /// yet, the human spirit remains unshaken. These brave defenders of Kryta continue to fight with every ounce of their
    /// strength.</summary>
    Human,

    /// <summary>This race of towering hunters experienced a great defeat when the Ice Dragon drove them from their glacial
    /// homeland. Nevertheless, they won't let one lost battle—however punishing—dampen their enthusiasm for life and the hunt.
    /// They know that only the ultimate victor achieves legendary rewards.</summary>
    Norn,

    /// <summary>Sylvari are not born. They awaken beneath the Pale Tree with knowledge gleaned in their pre-life Dream. These
    /// noble beings travel, seeking adventure and pursuing quests. They struggle to balance curiosity with duty, eagerness
    /// with chivalry, and warfare with honor. Magic and mystery entwine to shape the future of this race that has so recently
    /// appeared.</summary>
    Sylvari
}
