using System;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

namespace GW2SDK.Extensions
{
    public static class JsonSerializerSettingsExtensions
    {
        public static JsonSerializerSettings WithMissingMemberHandling([NotNull] this JsonSerializerSettings settings,
            MissingMemberHandling missingMemberHandling)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            settings.MissingMemberHandling = missingMemberHandling;
            return settings;
        }
    }
}
