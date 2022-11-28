using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.MailCarriers;

[PublicAPI]
[DataTransferObject]
public sealed record MailCarrier
{
    public required int Id { get; init; }

    public required IReadOnlyCollection<int> UnlockItems { get; init; }

    public required int Order { get; init; }

    public required string Icon { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<MailCarrierFlag> Flags { get; init; }
}
