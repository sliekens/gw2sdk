namespace GW2SDK.Features.Achievements
{
    public sealed class MasteryReward : AchievementReward
    {
        public int Id { get; set; }

        public MasteryRegionName Region { get; set; }
    }
}