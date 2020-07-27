using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Titles.Impl
{
    public class TitleJsonReader : JsonObjectReader<Title>
    {
        public static JsonObjectReader<Title> Instance { get; } = new TitleJsonReader();

        private TitleJsonReader()
        {
            Map("id",   to => to.Id);
            Map("name", to => to.Name);
            // This property should not be used because some titles can be unlocked by more than one achievement. Use 'achievements' instead.
            Ignore("achievement");
            Map("achievements", to => to.Achievements, MappingSignificance.Optional);
            Map("ap_required",  to => to.AchievementPointsRequired);
            Compile();
        }
    }
}