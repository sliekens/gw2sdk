using System;
using System.Collections.Generic;

using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.MailCarriers;

[PublicAPI]
[DataTransferObject]
public sealed record MailCarrier
{
    public int Id { get; init; }

    public IReadOnlyCollection<int> UnlockItems { get; init; } = Array.Empty<int>();

    public int Order { get; init; }

    public string Icon { get; init; } = "";

    public string Name { get; init; } = "";

    public IReadOnlyCollection<MailCarrierFlag> Flags { get; init; } = Array.Empty<MailCarrierFlag>();
}