using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Builds.Impl
{
    internal sealed class BuildJsonReader : JsonObjectReader<Build>
    {
        private BuildJsonReader()
        {
            Map("id", build => build.Id);
            Compile();
        }

        internal static BuildJsonReader Instance { get; } = new BuildJsonReader();
    }
}
