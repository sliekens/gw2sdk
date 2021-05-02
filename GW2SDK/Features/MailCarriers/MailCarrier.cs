using GW2SDK.Annotations;

namespace GW2SDK.MailCarriers
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record MailCarrier
    {
        public int Id { get; init; }

        public int[] UnlockItems { get; init; } = new int[0];

        public int Order { get; init; }

        public string Icon { get; init; } = "";

        public string Name { get; init; } = "";

        public MailCarrierFlag[] Flags { get; init; } = new MailCarrierFlag[0];
    }
}
