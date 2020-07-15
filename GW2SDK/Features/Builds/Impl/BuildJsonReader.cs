using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Builds.Impl
{
    internal sealed class BuildJsonReader : JsonObjectReader<Build>
    {
        public BuildJsonReader()
        {
            Map("id", build => build.Id);
            Compile();
        }
    }
}
