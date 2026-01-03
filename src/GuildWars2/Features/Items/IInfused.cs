namespace GuildWars2.Items;

/// <summary>The interface for items with an extra infusion slot.</summary>
public interface IInfused
{
    /// <summary>If the current item is upgraded, this collection contains the IDs of possible source items.</summary>
    IReadOnlyCollection<InfusionSlotUpgradeSource> UpgradesFrom { get; }
}
