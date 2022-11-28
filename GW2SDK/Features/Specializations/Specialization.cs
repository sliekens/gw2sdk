using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Specializations;

[PublicAPI]
[DataTransferObject]
public sealed record Specialization
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required ProfessionName Profession { get; init; }

    public required bool Elite { get; init; }

    public required IReadOnlyCollection<int> MinorTraits { get; init; }

    public required IReadOnlyCollection<int> MajorTraits { get; init; }

    public required int? WeaponTrait { get; init; }

    public required string Icon { get; init; }

    public required string Background { get; init; }

    public required string ProfessionIconBig { get; init; }

    public required string ProfessionIcon { get; init; }
}
