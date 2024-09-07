namespace GuildWars2.Chat;

/// <summary>Information about selected specialization and traits.</summary>
/// <param name="Id">The specialization ID.</param>
/// <param name="AdeptTrait">The first trait.</param>
/// <param name="MasterTrait">The second trait.</param>
/// <param name="GrandmasterTrait">The third trait.</param>
[PublicAPI]
public record struct Specialization(
    int Id,
    SelectedTrait AdeptTrait,
    SelectedTrait MasterTrait,
    SelectedTrait GrandmasterTrait
);
