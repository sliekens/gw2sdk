using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Builds.Impl
{
    internal sealed class BuildJsonReader : JsonObjectReader<Build>
    {
        private BuildJsonReader()
        {
            Configure(MapBuild);
        }

        internal static IJsonReader<Build> Instance { get; } = new BuildJsonReader();

        private static void MapBuild(JsonObjectMapping<Build> build)
        {
            build.Map("id", to => to.Id);
        }
    }
}
