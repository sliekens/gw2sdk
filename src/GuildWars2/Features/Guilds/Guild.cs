using GuildWars2.Guilds.Emblems;

namespace GuildWars2.Guilds;

/// <summary>Information about a guild.</summary>
[DataTransferObject]
public sealed record Guild
{
    /// <summary>The guild's level. It increases from 0 to 69 by creating a guild hall and upgrading it.</summary>
    public required int? Level { get; init; }

    /// <summary>The guild's message of the day which is displayed to members when they log in. The maximum length is 999
    /// characters.</summary>
    public required string? MessageOfTheDay { get; init; }

    /// <summary>Influence was once the currency that guilds spent to purchase boons and other guild-wide upgrades. It has been
    /// replaced by <see cref="Favor" />.</summary>
    public required int? Influence { get; init; }

    /// <summary>Aetherium is a guild currency used to purchase virtually all guild upgrades. Aetherium is generated
    /// automatically by any guild that has acquired a guild hall and completed the Mine Excavation 1 upgrade.</summary>
    public required int? Aetherium { get; init; }

    /// <summary>Resonance was once a resource used by Scribes to speed up the assembly of a schematic by 1 hour. Resonance is
    /// now unobtainable and the processing time of all schematics is reduced to 30 seconds.</summary>
    public required int? Resonance { get; init; }

    /// <summary>Favor is a measure of the goodwill that a guild has earned from the people of Tyria. It is earned by
    /// completing weekly guild missions. Favor is used to purchase guild consumables and other guild-wide upgrades.  A maximum
    /// of 2000 Favor can be earned per week, with an overall cap of 6000 at any given time.</summary>
    public required int? Favor { get; init; }

    /// <summary>The current number of members in the guild.</summary>
    public required int? MemberCount { get; init; }

    /// <summary>The maximum number of members that can be in the guild.</summary>
    public required int? MemberCapacity { get; init; }

    /// <summary>The guild ID.</summary>
    public required string Id { get; init; }

    /// <summary>The name of the guild.</summary>
    public required string Name { get; init; }

    /// <summary>The 2 to 4 letter guild tag representing the guild.</summary>
    public required string Tag { get; init; }

    /// <summary>The guild's emblem, which is the symbol of the guild. The emblem is a combination of a foreground and
    /// background image, with customizable colors.</summary>
    public required GuildEmblem Emblem { get; init; }
}
