namespace GuildWars2.Items;

/// <summary>Information about a tool which can be used to salvage items.</summary>
[PublicAPI]
public sealed record SalvageTool : Item
{
    /// <summary>The number of charges the tool has. Each use of the tool consumes 1 charge.</summary>
    /// <remarks>Unbreakable tools have this value set to 1, even though they have unlimited charges.</remarks>
    public required int Charges { get; init; }

    /// <summary>Indicates whether the tool has unlimited charges.</summary>
    public bool Unbreakable => Charges == 1 && Flags.DeleteWarning;
}
