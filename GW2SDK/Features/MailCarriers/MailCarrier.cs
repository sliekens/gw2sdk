using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.MailCarriers
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record MailCarrier
    {
        public int Id { get; init; }

        public int[] UnlockItems { get; init; } = Array.Empty<int>();

        public int Order { get; init; }

        public string Icon { get; init; } = "";

        public string Name { get; init; } = "";

        public MailCarrierFlag[] Flags { get; init; } = Array.Empty<MailCarrierFlag>();
    }
}
