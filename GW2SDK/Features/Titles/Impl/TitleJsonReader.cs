using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Titles.Impl
{
    public class TitleJsonReader : JsonObjectReader2<Title>
    {
        public static IJsonReader<Title> Instance { get; } = new TitleJsonReader();

        private TitleJsonReader()
        {
            Configure(
                title =>
                {
                    title.Map("id",   to => to.Id);
                    title.Map("name", to => to.Name);
                    // This property should not be used because some titles can be unlocked by more than one achievement. Use 'achievements' instead.
                    title.Ignore("achievement");
                    title.Map("achievements", to => to.Achievements, MappingSignificance.Optional);
                    title.Map("ap_required",  to => to.AchievementPointsRequired);
                });
            
        }
    }
}