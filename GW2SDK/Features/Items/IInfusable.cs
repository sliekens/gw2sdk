namespace GuildWars2.Items;


/// <summary>The interface for items that can be infused (or attuned) to add one extra infusion slot.</summary>
public interface IInfusable
{
    /// <summary>If the current item is used in the Mystic Forge to infuse (or attune) equipment, this collection contains the IDs
    /// of the infused (or attuned) items. Each item in the collection represents a different recipe.</summary>
    IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesInto { get; }
}
