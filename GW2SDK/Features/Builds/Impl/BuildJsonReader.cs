using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Builds.Impl
{
    internal sealed class BuildJsonReader : JsonObjectReader2<Build>
    {
        private BuildJsonReader()
        {
            Configure(
                build =>
                {
                    build.Map("id", to => to.Id);
                }
            );
        }

        internal static IJsonReader<Build> Instance { get; } = new BuildJsonReader();
    }
}
