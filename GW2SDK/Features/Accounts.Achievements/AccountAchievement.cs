using GW2SDK.Annotations;

namespace GW2SDK.Accounts.Achievements
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class AccountAchievement
    {
        public int Id { get; set; }

        [CanBeNull]
        public int[] Bits { get; set; }

        public int Current { get; set; }

        public int Max { get; set; }

        public bool Done { get; set; }

        public int? Repeated { get; set; }

        public bool? Unlocked { get; set; }
    }
}
