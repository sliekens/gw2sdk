using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Accounts.Achievements
{
    [DataTransferObject]
    public sealed class Achievement
    {
        public int Id { get; set; }

        [CanBeNull]
        public int[] Bits { get; set; }

        public int Current { get; set; }

        public int Max { get; set; }

        public bool Done { get; set; }

        [CanBeNull]
        public int? Repeated { get; set; }

        [CanBeNull]
        public bool? Unlocked { get; set; }
    }
}
